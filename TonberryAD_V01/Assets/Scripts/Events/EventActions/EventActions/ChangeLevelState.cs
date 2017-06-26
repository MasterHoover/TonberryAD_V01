using UnityEngine;
using System.Collections;

public class ChangeLevelState : EventAction 
{
	public enum StateChange
	{
		Trigger,
		Untrigger
	}

	public string levelName;
	public string stateName;
	public StateChange action;

	public override void CopyInfo (SerializedClass info)
	{
		LevelStateChangeInfo i = (LevelStateChangeInfo) info;
		levelName = i.LevelName;
		stateName = i.ModifierName;
		action = i.Action;
	}

	public override SerializedClass GetSerializedInfo ()
	{
		return new LevelStateChangeInfo (levelName, stateName, action);
	}

	public override void PlayAction (int uniqueID)
	{
		Debug.Log ("Play Action : LevelStateChange " + ((LevelStateChangeInfo) GetSerializedInfo ()).ModifierName);
		GameManager.Instance.ChangeLevelState ((LevelStateChangeInfo) GetSerializedInfo ());
		SendOverStatusToPlayer (uniqueID);
	}
}
