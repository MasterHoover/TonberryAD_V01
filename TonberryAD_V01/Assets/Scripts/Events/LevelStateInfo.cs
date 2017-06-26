using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelStateInfo 
{
	private string levelName;
	private List<LevelState.LevelModifier> modifierStates;

	public LevelStateInfo (string levelName)
	{
		this.levelName = levelName;
	}

	public LevelStateInfo (string levelName, LevelState.LevelModifier[] modifierStates) 
		: this (levelName, new List<LevelState.LevelModifier> (modifierStates)){}

	public LevelStateInfo (string levelName, List<LevelState.LevelModifier> modifierStates)
	{
		this.levelName = levelName;
		this.modifierStates = modifierStates;
	}

	public List<LevelState.LevelModifier> ModifierStates
	{
		get{return modifierStates;}
		set{modifierStates = value;}
	}

	public void ChangeState (string modName, ChangeLevelState.StateChange action)
	{
		if (action == ChangeLevelState.StateChange.Trigger)
		{
			Trigger (modName);
		}
		else if (action == ChangeLevelState.StateChange.Untrigger)
		{
			Untrigger (modName);
		}
		else
		{
			Debug.LogWarning ("LevelStateInfo/ChangeState (string, LevelStateChange.StateChange) : action is unknown.");
		}
	}

	public void Trigger (string modName)
	{
		int index = modifierStates.IndexOf (new LevelState.LevelModifier (modName));
		if (index != -1)
		{
			modifierStates[index].Activate ();
		}
		else
		{
			Debug.LogWarning ("LevelStateInfo/Trigger (string) : [" + modName + "] doesn't exist for level " + levelName);
		}
	}

	public void Untrigger (string modName)
	{
		int index = modifierStates.IndexOf (new LevelState.LevelModifier (modName));
		if (index != -1)
		{
			modifierStates[index].Deactivate ();
		}
		else
		{
			Debug.LogWarning ("LevelStateInfo/Untrigger (string) : [" + modName + "] doesn't exist for level " + levelName);
		}
	}

	public string LevelName
	{
		get{return levelName;}
	}

	public override bool Equals (object obj)
	{
		return levelName.Equals (((LevelStateInfo) obj).levelName);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}
}
