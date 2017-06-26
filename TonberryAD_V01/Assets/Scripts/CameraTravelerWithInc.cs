using UnityEngine;
using System.Collections;

public class CameraTravelerWithInc : MonoBehaviour 
{
	private Vector3 targetSpot;
	public float travelingMaxSpeed;
	public float travelingMinSpeed;
	public float travelingDelay;
	public float travelingInc;
	public float travelingDec;
	private Vector3 camOffset;
	private bool traveling;
	private bool targetMoved;
	private float currentSpeed;
	public float distanceFromPlayer = 10f;

	void LateUpdate ()
	{
		Vector3 targetNewPos = GameManager.Instance.Player.transform.position + camOffset;
		float distanceFromTarget = Vector3.Distance (targetSpot, Camera.main.transform.position);
		targetMoved = targetSpot != targetNewPos;
		targetSpot = targetNewPos;
		Debug.DrawLine (GameManager.Instance.Player.transform.position, targetSpot);
			
		if (!traveling)
		{
			if(distanceFromTarget != 0f && !IsInvoking ("StartTraveling"))
			{
				Invoke ("StartTraveling", travelingDelay);
			}
		}
		else
		{
			if(targetMoved)
			{
				float newSpeed = currentSpeed + travelingInc * Time.deltaTime;
				if (newSpeed < travelingMaxSpeed)
				{
					currentSpeed = newSpeed;
				}
				else
				{
					currentSpeed = travelingMaxSpeed;
				}
			}
			else
			{
				float newSpeed = currentSpeed - travelingDec * Time.deltaTime;
				if (newSpeed > travelingMinSpeed)
				{
					currentSpeed = newSpeed;
				}
				else
				{
					currentSpeed = travelingMinSpeed;
				}
			}

			float travelingDistance = currentSpeed * Time.deltaTime;
			if(distanceFromTarget > travelingDistance)
			{
				Vector3 travelingDirection = (targetSpot - Camera.main.transform.position).normalized;
				Camera.main.transform.Translate (travelingDirection * currentSpeed * Time.deltaTime, Space.World);
			}
			else
			{
				Camera.main.transform.position = targetSpot;
				currentSpeed = 0f;
				traveling = false;
			}
		}
	}

	public void UpdateCamOffset ()
	{
		camOffset = Camera.main.transform.position - GameManager.Instance.Player.transform.position;
	}

	public void CenterPlayer ()
	{
		/*
		Vector3 camForward = Camera.main.transform.forward;
		Vector3 playerPos = GameManager.Instance.Player.transform.position;
		Vector3 camPosition = Camera.main.transform.position;
		Vector3 worldDown = Vector3.down;

		float heightOffset = Mathf.Abs (camPosition.y - playerPos.y);
		float angleInDegrees = Vector3.Angle (camForward, worldDown);
		float angleInRad = angleInDegrees * Mathf.Deg2Rad;
		Debug.Log ("Angle in degree : " + angleInDegrees);
		Debug.Log ("Angle in rad : " + angleInRad);
		float hyp = heightOffset / Mathf.Sin (angleInRad); // Amplitude
		Vector3 targetPos = playerPos + (- camForward * hyp);
		Camera.main.transform.position = targetPos;
		*/
		Camera.main.transform.position = GameManager.Instance.Player.transform.position - Camera.main.transform.forward * distanceFromPlayer;
	}

	void StartTraveling ()
	{
		traveling = true;
	}
}
