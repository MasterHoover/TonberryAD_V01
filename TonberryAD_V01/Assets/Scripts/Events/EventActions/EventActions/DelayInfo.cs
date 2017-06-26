using UnityEngine;
using System.Collections;

public class DelayInfo : SerializedClass 
{
	private float duration;

	public DelayInfo (float duration)
	{
		this.duration = duration;
	}

	public float Duration
	{
		get{return duration;}
	}
}
