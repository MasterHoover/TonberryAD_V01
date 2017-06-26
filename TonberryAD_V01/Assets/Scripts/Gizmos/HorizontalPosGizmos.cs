using UnityEngine;
using System.Collections;

public class HorizontalPosGizmos : CustomGizmos 
{
	public Color color = Color.green;
	public float distance = 2f;

	protected override void DrawGizmos ()
	{
		Gizmos.color = color;
		Gizmos.DrawLine (transform.position + Vector3.down * distance, transform.position + Vector3.up * distance);
	}
}
