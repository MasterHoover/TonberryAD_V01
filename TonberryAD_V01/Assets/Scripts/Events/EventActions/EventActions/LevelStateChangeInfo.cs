using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelStateChangeInfo : SerializedClass 
{
	private string levelName;
	private string modifierName;
	private ChangeLevelState.StateChange action;

	public LevelStateChangeInfo (string levelName, string stateName, ChangeLevelState.StateChange action)
	{
		this.levelName = levelName;
		this.modifierName = stateName;
		this.action = action;
	}

	public string LevelName
	{
		get{return levelName;}
		set{levelName = value;}
	}

	public string ModifierName
	{
		get{return modifierName;}
		set{modifierName = value;}
	}

	public ChangeLevelState.StateChange Action
	{
		get{return action;}
		set{action = value;} 
	}
}
