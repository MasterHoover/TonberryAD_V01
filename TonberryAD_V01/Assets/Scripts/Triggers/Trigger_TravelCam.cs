using UnityEngine;
using System.Collections;

public class Trigger_TravelCam : EventTrigger 
{
	protected override void LaunchEnterEvent (Collider col)
	{
		CameraManager.Instance.TravelCamera ();
	}

	protected override void LaunchExitEvent (Collider col)
	{
		CameraManager.Instance.FixCamera ();
	}
}
