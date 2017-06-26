using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraViewInfo
{
	private CameraManager.CameraViewType type;
	private Vector3 cameraPosition;
	private Quaternion cameraRotation;
	private Vector3 orientationPos;
	private Quaternion orientationRot;
	private float camDistance;
	
	public CameraViewInfo (CameraManager.CameraViewType type, Transform camReference) 
		: this (type, camReference.position, camReference.rotation) {}

	public CameraViewInfo (CameraManager.CameraViewType type, Transform camReference, Transform orientationReference)
		: this (type, camReference.position, camReference.rotation, orientationReference.position, orientationReference.rotation) {}

	public CameraViewInfo (CameraManager.CameraViewType type, Vector3 cameraPosition, Quaternion cameraRotation)
	{
		this.type = type;
		this.cameraPosition = cameraPosition;
		this.cameraRotation = cameraRotation;
	}

	public CameraViewInfo (CameraManager.CameraViewType type, Vector3 cameraPosition, Quaternion cameraRotation, 
		Vector3 orientationPos, Quaternion orientationRot)
	{
		this.type = type;
		this.cameraPosition = cameraPosition;
		this.cameraRotation = cameraRotation;
		this.orientationPos = orientationPos;
		this.orientationRot = orientationRot;
	}
}
