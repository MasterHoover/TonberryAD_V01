using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UnknownLevelStateInfo 
{
	private string levelName;
	private List<LevelState.UnknownLevelModifier> unknownLevelModifiers = new List<LevelState.UnknownLevelModifier> ();

	public UnknownLevelStateInfo (string levelName)
	{
		this.levelName = levelName;	
	}

	public UnknownLevelStateInfo (string levelName, string modifierName)
	{
		this.levelName = levelName;
		int index = unknownLevelModifiers.IndexOf (new LevelState.UnknownLevelModifier (modifierName));
		if (index == -1)
		{
			unknownLevelModifiers.Add (new LevelState.UnknownLevelModifier (modifierName));
		}
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
		int index = unknownLevelModifiers.IndexOf (new LevelState.UnknownLevelModifier (modName));
		if (index != -1)
		{
			unknownLevelModifiers[index].Activate ();
		}
		else
		{
			Debug.LogWarning ("LevelStateInfo/Trigger (string) : [" + modName + "] doesn't exist for level " + levelName);
		}
	}

	public void Untrigger (string modName)
	{
		int index = unknownLevelModifiers.IndexOf (new LevelState.UnknownLevelModifier (modName));
		if (index != -1)
		{
			unknownLevelModifiers[index].Deactivate ();
		}
		else
		{
			Debug.LogWarning ("LevelStateInfo/Untrigger (string) : [" + modName + "] doesn't exist for level " + levelName);
		}
	}

	public override bool Equals (object obj)
	{
		return levelName.Equals (((UnknownLevelStateInfo) obj).levelName);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public string LevelName
	{
		get{return levelName;}
	}

	public List<LevelState.UnknownLevelModifier> UnknownLevelModifiers
	{
		get{return unknownLevelModifiers;}
	}
}
