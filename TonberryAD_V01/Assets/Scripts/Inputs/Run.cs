using UnityEngine;
using System.Collections;

public class Run : InputAction 
{
	public const float DEFAULT_RUNNING_SPEED = 5f;
	public float runningSpeed = DEFAULT_RUNNING_SPEED;
	public ControllableCharacter controllableCharacter;
	public Animator charAnimator;

	void Awake ()
	{
		if (controllableCharacter == null)
		{
			Debug.LogWarning ("Run(" + gameObject.name + ")/Awake () : No controllable character set as variable to use the Run action. " +
				"Running won't change character speed.");
		}
		if (charAnimator == null)
		{
			Debug.LogWarning ("Run(" + gameObject.name + ")/Awake () : No animator set as variable to use the Run action. " +
				"Running won't change the character's sprite.");
		}
		if (controllableCharacter == null && charAnimator == null)
		{
			Debug.LogWarning ("Run(" + gameObject.name + ")/Awake () : No variables has been set. Running won't do anything. Disabling script.");
			enabled = false;
		}
	}

	protected override void ButtonAction ()
	{
		controllableCharacter.RunningSpeed = runningSpeed;
		controllableCharacter.Running = true;
	}

	protected override void NotButtonAction ()
	{
		controllableCharacter.Running = false;
	}

	public override void ForceExit ()
	{
		controllableCharacter.Running = false;
	}
}
