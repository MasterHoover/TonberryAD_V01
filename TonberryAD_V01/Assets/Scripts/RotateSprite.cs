using UnityEngine;
using System.Collections;

public class RotateSprite : MonoBehaviour 
{
	public ControllableCharacter movingScript;
	public bool spinning;
	private float rotationSpeed;
	private float rotationAmount;
	private float targetAngle;
	public GameObject reference;

	void Awake ()
	{
		if (movingScript == null)
		{
			Debug.LogWarning ("RotateSprite(" + gameObject.name + ")/Awake () : controllableCharacter is not assigned. Disabling script");
			enabled = false;
		}
	}

	void Update ()
	{
		if (spinning)
		{
			//Debug.Log ("Target angle is : " + targetAngle);
			float speed = rotationSpeed;
			speed *= Time.deltaTime;
			if (rotationAmount + Mathf.Abs (speed) < targetAngle)
			{
				SetRotationFromAngle (speed);
				rotationAmount += Mathf.Abs(speed);
			}
			else
			{
				//Debug.Log ("Spin is over");
				SetRotationFromAngle (speed > 0f ? targetAngle - rotationAmount : (targetAngle - rotationAmount));
				spinning = false;
			}
		}
	}

	public void RotateLeft (float targetAngle, float speed)
	{
		rotationSpeed = -Mathf.Abs (speed);
		this.targetAngle = Mathf.Abs (targetAngle);
		spinning = true;
		rotationAmount = 0f;
	}

	public void RotateLeft (float targetAngle)
	{
		RotateLeft (targetAngle, float.PositiveInfinity);
	}

	public void RotateRight (float targetAngle, float speed)
	{
		rotationAmount = 0f;
		this.targetAngle = Mathf.Abs (targetAngle);
		rotationSpeed = Mathf.Abs (speed);
		spinning = true;
	}

	public void RotateRight (float targetAngle)
	{
		RotateRight (targetAngle, float.PositiveInfinity);
	}

	private Vector3 GetDirectionFromRotatingYAxis (Vector3 fromVector, float angle)
	{
		Vector3 returnValue = Vector3.zero;
		GameObject dummy = (GameObject) Instantiate (reference, movingScript.transform.position, Quaternion.identity);
		dummy.transform.forward = movingScript.Direction;
		dummy.transform.Rotate (0f, angle, 0f, Space.Self);
		returnValue = dummy.transform.forward;
		DestroyImmediate (dummy);
		return returnValue;
	}

	private void SetRotationFromAngle (float angle)
	{
		movingScript.Direction = GetDirectionFromRotatingYAxis (movingScript.Direction, angle);
	}

	public void SpinLeft (float speed)
	{
		if (IsInvoking ("StopSpinning"))
		{
			CancelInvoke ("StopSpinning");
		}
		spinning = true;
		rotationSpeed = -Mathf.Abs (speed);
		targetAngle = float.PositiveInfinity;
	}

	public void SpinRight (float speed)
	{
		if (IsInvoking ("StopSpinning"))
		{
			CancelInvoke ("StopSpinning");
		}
		spinning = true;
		rotationSpeed = Mathf.Abs (speed);
		targetAngle = float.PositiveInfinity;
	}

	public void SpinLeft (float speed, float duration)
	{
		if (IsInvoking ("StopSpinning"))
		{
			CancelInvoke ("StopSpinning");
		}
		spinning = true;
		rotationSpeed = -Mathf.Abs (speed);
		targetAngle = float.PositiveInfinity;
		Invoke ("StopSpinning", duration);
	}

	public void SpinRight (float speed, float duration)
	{
		if (IsInvoking ("StopSpinning"))
		{
			CancelInvoke ("StopSpinning");
		}
		spinning = true;
		rotationSpeed = Mathf.Abs (speed);
		targetAngle = float.PositiveInfinity;
		Invoke ("StopSpinning", duration);
	}

	public void StopSpinning ()
	{
		movingScript.charAnimator.ChangeToIdle ();
		spinning = false;
	}

	public void RotateRight90 (float speed)
	{
		RotateRight (90f, speed);
	}

	public void RotateRight90 ()
	{
		RotateRight (90f);
	}

	public void RotateLeft90 (float speed)
	{
		RotateLeft (90f, speed);
	}

	public void RotateLeft90 ()
	{
		RotateLeft (90f);
	}

	public void Rotate180 ()
	{
		RotateLeft (180f);
	}

	public void Rotate180 (float speed, bool left)
	{
		if (left)
		{
			RotateLeft (180f, speed);
		}
		else
		{
			RotateRight (180f, speed);
		}
	}
}
