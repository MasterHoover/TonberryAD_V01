using UnityEngine;
using System.Collections;

public class GameEventDelayer : MonoBehaviour 
{
	private int uniqueID;
	private Delay script;

	public void StartDelay (float duration)
	{
		if (IsInvoking ("DelayIsOver"))
		{
			CancelInvoke ("DelayIsOver");
			Debug.Log ("Had to cancel invoke. Id : " + uniqueID);
		}
		Invoke ("DelayIsOver", duration);
	}

	public int UniqueID
	{
		get{return uniqueID;}
		set{uniqueID = value;}
	}

	void DelayIsOver ()
	{
		script.SendMessage ("DelayEnded", uniqueID);	
	}

	public Delay Script
	{
		get{return script;}
		set{script = value;}
	}
}
