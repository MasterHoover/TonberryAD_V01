using UnityEngine;
using System.Collections;

public class CameraTraveler : MonoBehaviour 
{
	//private Vector3 camOffset;
	public float distanceFromPlayer = 10f;

	void LateUpdate ()
	{
		Vector3 targetSpot = GameManager.Instance.Player.transform.position - Camera.main.transform.forward * distanceFromPlayer;
		Camera.main.transform.position = targetSpot;
	}
}
