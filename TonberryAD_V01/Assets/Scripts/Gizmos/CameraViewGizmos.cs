using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CameraView))]
public class CameraViewGizmos : CustomGizmos 
{
	protected override void DrawGizmos ()
	{
		CameraView script = GetComponent<CameraView> ();
		if (script.cameraPosition != null)
		{
			Camera.main.transform.position = script.cameraPosition.position;
			Camera.main.transform.rotation = script.cameraPosition.rotation;
		}
	}
}
