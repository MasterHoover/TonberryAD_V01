using UnityEngine;
using System.Collections;

public class StandPoint : MonoBehaviour 
{
	public PlayerPosition playerPosition;
	public CameraPosition cameraPosition;

	void OnDrawGizmos ()
	{
		Gizmos.DrawLine (playerPosition.transform.position, cameraPosition.transform.position);
	}
}
