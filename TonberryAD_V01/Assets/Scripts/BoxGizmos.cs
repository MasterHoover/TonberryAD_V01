using UnityEngine;
using System.Collections;

public class BoxGizmos : CustomGizmos 
{
	public Color color = Color.grey;
	public float alpha = 0.8f;

	protected override void DrawGizmos ()
	{
		Color withAlpha = color;
		withAlpha.a = alpha;
		Gizmos.color = withAlpha; 
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawCube (Vector3.zero, Vector3.one);
	}
}
