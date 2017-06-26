using UnityEngine;
using System.Collections;

public class DebugLogInfo : SerializedClass 
{
	private string message;

	public DebugLogInfo (string message)
	{
		this.message = message;	
	}

	public string Message
	{
		get{return message;}
	}
}
