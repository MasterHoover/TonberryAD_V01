using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelState : MonoBehaviour, ISerializable<LevelStateInfo>
{
	public List<LevelModifier> modifiers = new List<LevelModifier> ();

	void Start ()
	{
		Debug.Log ("Start () [" + name + "]");
		for (int i = 0; i < modifiers.Count; i++)
		{
			bool active = true;

			for (int j = 0; active && j < modifiers[i].switches.Length; j++)
			{
				if (!modifiers[i].switches[j])
				{
					active = false;
				}
			}
			if (active)
			{
				Debug.Log ("Modifier " + modifiers[i].modifierName + " is active");
				EventPlayer.Instance.PlayEvent(modifiers[i].events);
			}
			else
			{
				Debug.Log ("Modifier " + modifiers[i].modifierName + " is not active.");
			}
		}
	}

	public LevelStateInfo GetSerializedInfo ()
	{
		return new LevelStateInfo (GameManager.Instance.CurrentLevelName, modifiers);
	}

	public void Copy (List<LevelModifier> info)
	{
		Debug.Log ("Copy () [" + name + "]");
		for (int i = 0; i < modifiers.Count; i++)
		{
			modifiers[i].switches = info[i].switches;
		}
	}

	public void Copy (string modifierName, int activationCount)
	{
		int index = modifiers.IndexOf (new LevelModifier (modifierName));
		if (index != -1)
		{
			while (activationCount != 0)
			{
				if (activationCount < 0)
				{
					modifiers[index].Deactivate ();
					activationCount++;
				}
				else
				{
					modifiers[index].Activate ();
					activationCount--;
				}
			}
		}
		else
		{
			Debug.LogWarning ("LevelState[" + gameObject.name + "]/Copy (string, int) : modifier of name [" + modifierName + "] doesn't exist.");
		}
	}

	public void ChangeState (string stateName, ChangeLevelState.StateChange action)
	{
		if (action == ChangeLevelState.StateChange.Trigger)
		{
			TriggerState (stateName);
		}
		else if (action == ChangeLevelState.StateChange.Untrigger)
		{
			UntriggerState (stateName);
		}
		else
		{
			Debug.LogWarning ("LevelState[" + name + "]/ChangeState (string, LevelStateChange.StateChange) : levelStateChange action is unknown.");
		}
	}

	public void TriggerState (string stateName)
	{
		int index = modifiers.IndexOf (new LevelModifier (stateName));
		if (index != -1)
		{
			modifiers[index].Activate ();
			if (modifiers[index].AllTrue ())
			{
				EventPlayer.Instance.PlayEvent (modifiers[index].events);
			}
		}
		else
		{
			Debug.LogWarning ("LevelState[" + name + "]/TriggerState (string) : state with name [" + stateName + "] doesn't exists.");
		}
	}

	public void TriggerState_All (string stateName, bool activateEvent)
	{
		int index = modifiers.IndexOf (new LevelModifier (stateName));
		if (index != -1)
		{
			modifiers[index].ActivateAll ();
			if (activateEvent)
			{
				EventPlayer.Instance.PlayEvent (modifiers[index].events);
			}
		}
		else
		{
			Debug.LogWarning ("LevelState[" + name + "]/TriggerState_All (string) : state with name [" + stateName + "] doesn't exists.");
		}
	}

	public void UntriggerState (string stateName)
	{
		int index = modifiers.IndexOf (new LevelModifier (stateName));
		if (index != -1)
		{
			modifiers[index].Deactivate ();
		}
		else
		{
			Debug.LogWarning ("LevelState[" + name + "]/UntriggerState (string) : state with name [" + stateName + "] doesn't exists.");
		}
	}

	public void AddLevelState (string stateName, int switchCount, bool activated)
	{
		modifiers.Add (new LevelModifier (stateName, switchCount, activated));
	}

	public void AddLevelState (string stateName, bool activated)
	{
		AddLevelState (stateName, 1, activated);
	}

	[System.Serializable]
	public class LevelModifier
	{
		public string modifierName;
		public bool[] switches;
		public EventPlayer.OngoingEvent.ActionStep[] events;

		public LevelModifier () {}

		public LevelModifier (string modifierName) : this (modifierName, 0, false)
		{
			this.modifierName = modifierName;
		}

		public LevelModifier (string modifierName, bool activated) : this (modifierName, 1, activated){}

		public LevelModifier (string modifierName, int switchCount, bool activated)
		{
			this.modifierName = modifierName;
			if (switchCount > 0)
			{
				switches = new bool[switchCount];
			}
			if (activated && switches != null && switches.Length > 0)
			{
				ActivateAll ();
			}
		}

		public void Activate ()
		{
			Debug.Log ("Activate ()");
			bool foundIt = false;
			for (int i = 0; !foundIt && i < switches.Length; i++)
			{
				if (!switches[i])
				{
					foundIt = true;
					switches[i] = true;
				}
			}
			if (!foundIt)
			{
				Debug.LogWarning ("LevelState/LevelModifier/Activate () : tried to activate a switch when they were all activated.");
			}
		}

		public void ActivateAll ()
		{
			for (int i = 0; i < switches.Length; i++)
			{
				switches[i] = true;
			}
		}

		public void Deactivate ()
		{
			bool foundIt = false;
			for (int i = 0; !foundIt && i < switches.Length; i++)
			{
				if (switches[i])
				{
					foundIt = true;
					switches[i] = false;
				}
			}
			if (!foundIt)
			{
				Debug.LogWarning ("LevelState/LevelModifier/Deactivate () : tried to deactivate a switch when they were all deactivated.");
			}
		}

		public bool AllTrue ()
		{
			for (int i = 0; i < switches.Length; i++)
			{
				if (!switches[i])
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals (object obj)
		{
			return modifierName.Equals (((LevelModifier) obj).modifierName);
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}

	/// <summary>
	/// Same as LevelModifier, but we don't know the number of switches.
	/// Only changes an int do know how many time we activate (positive value) or deactivate (negative) it.
	/// </summary>
	[System.Serializable]
	public class UnknownLevelModifier
	{
		private string modifierName;
		private int activationCount;

		public UnknownLevelModifier (string modifierName)
		{
			this.modifierName = modifierName;
		}

		public void Activate ()
		{
			activationCount++;
		}

		public void Deactivate ()
		{
			activationCount--;
		}

		public string ModifierName
		{
			get{return modifierName;}
		}

		public int ActivationCount
		{
			get{return activationCount;}
		}

		public override bool Equals (object obj)
		{
			return modifierName.Equals (((UnknownLevelModifier) obj).modifierName);
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}
}
