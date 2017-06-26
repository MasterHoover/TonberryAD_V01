using UnityEngine;
using System.Collections;

public class CardinalPointGizmos : CustomGizmos 
{
	public float linesLength = 3f;
	private Color northSideColor = Color.blue;
	private Color otherSidesColor = Color.red;

	protected override void DrawGizmos ()
	{
		northSideColor.a = 1f;
		otherSidesColor.a = 1f;

		Gizmos.color = northSideColor;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * linesLength);
	
		Gizmos.color = otherSidesColor;
		Gizmos.DrawLine (transform.position, transform.position - transform.forward * linesLength);
		Gizmos.DrawLine (transform.position, transform.position + transform.right * linesLength);
		Gizmos.DrawLine (transform.position, transform.position - transform.right * linesLength);
	}
}
