using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour, ISerializable<CameraViewInfo>
{
	public CameraManager.CameraViewType type;
	public Transform cameraPosition;
	public Transform orientation;
	public float camDistance;

	public CameraViewInfo GetSerializedInfo ()
	{
		if (orientation != null)
		{
			return new CameraViewInfo (type, cameraPosition, orientation);
		}
		return new CameraViewInfo (type, cameraPosition);
	}
}
