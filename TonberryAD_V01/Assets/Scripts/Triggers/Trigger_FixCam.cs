using UnityEngine;
using System.Collections;

public class Trigger_FixCam : EventTrigger 
{
	protected override void LaunchEnterEvent (Collider col)
	{
		CameraManager.Instance.FixCamera ();
	}

	protected override void LaunchExitEvent (Collider col)
	{
		CameraManager.Instance.TravelCamera ();
	}
}
