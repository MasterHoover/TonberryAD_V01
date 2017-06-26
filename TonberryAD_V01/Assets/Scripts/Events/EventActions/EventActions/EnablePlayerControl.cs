using UnityEngine;
using System.Collections;

public class EnablePlayerControl : EventAction 
{
	public override void CopyInfo (SerializedClass info){}

	public override SerializedClass GetSerializedInfo ()
	{
		return null;
	}

	public override void PlayAction (int uniqueID)
	{
		GameManager.Instance.EnableCharacterMovement ();
		SendOverStatusToPlayer (uniqueID);
	}
}
