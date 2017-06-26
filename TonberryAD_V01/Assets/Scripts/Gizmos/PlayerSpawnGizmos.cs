using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerSpawn))]
public class PlayerSpawnGizmos : CustomGizmos 
{
	private PlayerSpawn script;
	public Color cameraPositionColor = Color.yellow;
	public Color cardinalColor = Color.cyan;

	protected override void DrawGizmos ()
	{
		PlayerSpawn script = GetComponent <PlayerSpawn> ();
		if (script != null)
		{
			if (script.viewInfo != null)
			{
				if (script.viewInfo.cameraPosition != null)
				{
					Gizmos.color = cameraPositionColor;
					Gizmos.DrawLine (transform.position, script.viewInfo.cameraPosition.position);
				}

				if (script.viewInfo.orientation != null)
				{
					Gizmos.color = cardinalColor;
					Gizmos.DrawLine (transform.position, script.viewInfo.orientation.position);
				}
			}
		}
	}
}
