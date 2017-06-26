using UnityEngine;
using System.Collections;

public class Trigger_PlayEvent : EventTrigger 
{
	public EventPlayer.OngoingEvent.ActionStep[] eventToPlay;

	protected override void LaunchEnterEvent (Collider col)
	{
		EventPlayer.Instance.PlayEvent (eventToPlay);
	}
}
