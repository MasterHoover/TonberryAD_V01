using UnityEngine;
using System.Collections;

public class DebugLog : EventAction 
{
	public string message;

	public override SerializedClass GetSerializedInfo ()
	{
		return new DebugLogInfo (message);
	}

	public override void PlayAction (int uniqueID)
	{
		Debug.Log (message);
		SendOverStatusToPlayer (uniqueID);
	}

	public override void CopyInfo (SerializedClass info)
	{
		DebugLogInfo i = (DebugLogInfo) info;
		message = i.Message;
	}
}
