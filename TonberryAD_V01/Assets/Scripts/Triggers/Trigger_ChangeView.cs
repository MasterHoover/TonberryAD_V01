using UnityEngine;
using System.Collections;

public class Trigger_ChangeView : EventTrigger 
{
	public CameraView front;
	public CameraView back;

	public enum CamType
	{
		Fix,
		Travelling
	}

	protected override void LaunchEnterEvent (Collider col)
	{
		if (Vector3.Angle (transform.forward, col.gameObject.GetComponent<ControllableCharacter> ().Direction) > 90f) // Front
		{
			CameraManager.Instance.ChangeCameraView (front);
		}
		else
		{
			CameraManager.Instance.ChangeCameraView (back);
		}
	}

	protected override void LaunchExitEvent (Collider col)
	{
		if (Vector3.Angle (transform.forward, col.gameObject.GetComponent<ControllableCharacter> ().Direction) < 90f) // Back
		{
			CameraManager.Instance.ChangeCameraView (back);
		}
		else
		{
			CameraManager.Instance.ChangeCameraView (front);
		}
	}
}
