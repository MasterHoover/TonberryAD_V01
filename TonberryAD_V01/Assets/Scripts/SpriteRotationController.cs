using UnityEngine;
using System.Collections;

public class SpriteRotationController : MonoBehaviour 
{
	public RotateSprite rotator;
	public float spinningSpeed;
	public float fluidRotationSpeed;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.KeypadMultiply))
		{
			rotator.RotateRight90 ();
		}
		if (Input.GetKeyDown (KeyCode.KeypadDivide))
		{
			rotator.RotateLeft90 ();
		}
		if (Input.GetKeyDown (KeyCode.Keypad1))
		{
			rotator.SpinLeft (spinningSpeed);
		}
		if (Input.GetKeyDown (KeyCode.Keypad3))
		{
			rotator.SpinRight (spinningSpeed);	
		}

		if (Input.GetKeyDown (KeyCode.Keypad7))
		{
			rotator.Rotate180 (fluidRotationSpeed, true);
		}

		if (Input.GetKeyDown (KeyCode.Keypad9))
		{
			rotator.Rotate180 (fluidRotationSpeed, false);
		}

		if (Input.GetKeyDown (KeyCode.KeypadPlus))
		{
			if (IsInvoking ("StopSpinning"))
			{
				CancelInvoke ("StopSpinning");
			}
			rotator.StopSpinning ();
		}
	}
}
