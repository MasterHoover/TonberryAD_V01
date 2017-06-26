using UnityEngine;
using System.Collections;

public class CameraPositionGizmos : CustomGizmos 
{
	protected override void DrawGizmos ()
	{
		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
	}
}
