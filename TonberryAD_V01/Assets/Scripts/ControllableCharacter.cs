/*
Author : Olivier Reid

Class PlayerCharacter
This class is used to control a character in a 3D environment, using a character controller.
Here is its particularities :
- It automatically detects any collisions, as it uses a character controller.
- It automatically snaps to the nearest bottom collision.
- It has a walking and a running state, that can have different speeds.

*/
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class ControllableCharacter : MonoBehaviour
{
	public const float DEFAULT_WALKING_SPEED = 2f;

	public float walkingSpeed = DEFAULT_WALKING_SPEED;
	private float runningSpeed;
	private bool running;
	private bool moving;
	private CharacterController charController;
	public Character controlledCharacter;
	private InputAction[] inputs;
	public CharacterAnimator charAnimator;
	private Vector3 direction;

	void Awake ()
	{
		charController = GetComponent <CharacterController> ();
		GetActionInput ();
	}

	void Update ()
	{
		DoCharacterMovement ();
		GetActionInput ();
	}

	void GetActionInput ()
	{
		inputs = GetComponents<InputAction> ();
	}

	void DoCharacterMovement ()
	{
		// Move character
		float speed = !running ? walkingSpeed : runningSpeed;
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		moving = horizontal != 0f || vertical != 0f;

		if (moving)
		{
			direction = Vector3.zero;
			direction += GameManager.Instance.CardinalEast * horizontal;
			direction += GameManager.Instance.CardinalNorth * vertical;
			direction.Normalize ();
			charController.Move (direction * speed * Time.deltaTime);
			transform.forward = direction;
		}
	}
		
	public void EnableCharacter ()
	{
		EnableInputs ();
		enabled = true;	
	}

	public void DisableCharacter ()
	{
		Debug.Log ("DISABLE");
		moving = false;
		charAnimator.ChangeToIdle ();
		DisableInputs ();
		enabled = false;
	}

	private void EnableInputs ()
	{
		foreach (InputAction i in inputs)
		{
			i.enabled = true;
		}
	}

	private void DisableInputs ()
	{
		foreach (InputAction i in inputs)
		{
			i.ForceExit ();
			i.enabled = false;
		}
	}

	public bool Running
	{
		get{return running;}
		set{running = value;}
	}

	public float RunningSpeed
	{
		set{runningSpeed = value;}
	}

	public bool Moving
	{
		get{return moving;}
	}

	public Vector3 Direction
	{
		get{return direction;}
		set{direction = value;}
	}
}
