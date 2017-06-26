using UnityEngine;
using System.Collections;

public class EnableObject : EventAction 
{
	public GameObject toEnable;

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
		if (toEnable != null)
		{
			toEnable.SetActive (true);
		}
		else
		{
			Debug.LogWarning ("DestroyObject[" + gameObject.name + "] : object to enable is not assigned.");
		}
		SendOverStatusToPlayer (uniqueID);
	}
}
