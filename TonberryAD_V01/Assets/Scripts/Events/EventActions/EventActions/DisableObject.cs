using UnityEngine;
using System.Collections;

public class DisableObject : EventAction 
{
	public GameObject objectToDisable;

	public override void CopyInfo (SerializedClass info)
	{
		throw new System.NotImplementedException ();
	}

	public override SerializedClass GetSerializedInfo ()
	{
		throw new System.NotImplementedException ();
	}

	public override void PlayAction (int uniqueID)
	{
		if (objectToDisable != null)
		{
			objectToDisable.SetActive (false);
		}
		else
		{
			Debug.LogWarning ("DestroyObject[" + gameObject.name + "] : object to disable is not assigned.");
		}
		SendOverStatusToPlayer (uniqueID);
	}
}
