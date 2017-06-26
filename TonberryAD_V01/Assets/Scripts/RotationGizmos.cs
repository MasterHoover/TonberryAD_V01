using UnityEngine;
using System.Collections;

public class RotationGizmos : CustomGizmos 
{
	public bool showOnlyForward;
	public float lineLength = 3f;
	public Color forwardColor = Color.blue;
	public Color otherColor = Color.black;

	protected override void DrawGizmos ()
	{
		forwardColor.a = 1f;
		Gizmos.color = forwardColor;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * lineLength);

		if (!showOnlyForward)
		{
			otherColor.a = 1f;
			Gizmos.color = otherColor;
			Gizmos.DrawLine (transform.position, transform.position - transform.forward * lineLength);
			Gizmos.DrawLine (transform.position, transform.position + transform.right * lineLength);
			Gizmos.DrawLine (transform.position, transform.position - transform.right * lineLength);
		}
	}
}
