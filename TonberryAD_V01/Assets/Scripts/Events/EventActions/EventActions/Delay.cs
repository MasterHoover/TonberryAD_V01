using UnityEngine;
using System.Collections;

public class Delay : EventAction 
{
	public float duration;

	public override SerializedClass GetSerializedInfo ()
	{
		return new DelayInfo (duration);
	}

	public override void PlayAction (int uniqueID)
	{
		GameEventDelayer delayer = gameObject.AddComponent<GameEventDelayer> ();
		delayer.UniqueID = uniqueID;
		delayer.Script = this;
		delayer.StartDelay (duration);
	}

	public void DelayEnded (int uniqueID)
	{
		SendOverStatusToPlayer (uniqueID);
	}

	public override void CopyInfo (SerializedClass info)
	{
		DelayInfo i = (DelayInfo) info;
		duration = i.Duration;
	}
}
