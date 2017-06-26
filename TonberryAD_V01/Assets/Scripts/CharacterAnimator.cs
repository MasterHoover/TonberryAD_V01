using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class CharacterAnimator : MonoBehaviour 
{
	public ControllableCharacter movingScript;
	private Animator animator;

	private const string FACING_LEFT_BOOL = "FacingLeft";
	private const string MOVING_BOOL = "Moving";
	private const string SIDED_BOOL = "Sided";
	private const string FRONT_BOOL = "FacingFront";
	private const string RUNNING_BOOL = "Running";
	private const string CHANGE_STATE_TRIGGER = "ChangeState";

	AnimatorStateInfo info;
	private bool sided;
	private bool facingLeft;
	private bool facingFront;
	private bool moving;
	private bool running;

	private AnimationState state;

	void Awake ()
	{
		if (movingScript == null)
		{
			Debug.LogWarning ("CharacterAnimator(" + gameObject.name + ")/Awake () : No moving script assigned. Disabling script.");
			enabled = false;
		}
		animator = GetComponent<Animator> ();
	}
		
	void LateUpdate ()
	{
		UpdateCharacterParams ();
	}

	public void UpdateCharacterParams ()
	{
		UpdateCharacterParams (movingScript.Direction, movingScript.Moving, movingScript.Running);
	}

	public void UpdateCharacterParams (Vector3 direction, bool moving, bool running)
	{
		Vector3 fakeDir = new Vector3 (direction.x, 0f, direction.z);
		float angleWithCamRight = Vector3.Angle (fakeDir, Camera.main.transform.right);
		sided = angleWithCamRight < 45f || angleWithCamRight > 135f;
		if (sided)
		{
			facingLeft = Vector3.Angle (fakeDir, Camera.main.transform.right) > 90f;
		}
		else
		{
			float angleWithCamForward = Vector3.Angle (fakeDir, Camera.main.transform.forward);
			facingFront = !sided && angleWithCamForward > 90f;
		}
		this.moving = moving;
		this.running = running;

		if (StateChanged ())
		{
			animator.SetBool (FACING_LEFT_BOOL, facingLeft);
			animator.SetBool (MOVING_BOOL, moving);
			animator.SetBool (SIDED_BOOL, sided);
			animator.SetBool (FRONT_BOOL, facingFront);
			animator.SetBool (RUNNING_BOOL, running);
			animator.SetTrigger (CHANGE_STATE_TRIGGER);
		}
	}

	private bool StateChanged ()
	{
		return animator.GetBool (FACING_LEFT_BOOL) != facingLeft
		|| animator.GetBool (MOVING_BOOL) != moving
		|| animator.GetBool (SIDED_BOOL) != sided
		|| animator.GetBool (FRONT_BOOL) != facingFront
			|| animator.GetBool (RUNNING_BOOL) != running;
	}

	private CharacterState GetCurrentState ()
	{
		if (sided)
		{
			if (facingLeft)
			{
				if (!moving)
				{
					return CharacterState.Left_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Left_Walking;
					}
					else
					{
						return CharacterState.Left_Running;
					}
				}
			}
			else
			{
				if (!moving)
				{
					return CharacterState.Right_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Right_Walking;
					}
					else
					{
						return CharacterState.Right_Running;
					}
				}
			}
		}
		else
		{
			if (facingFront)
			{
				if(!moving)
				{
					return CharacterState.Front_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Front_Walking;
					}
					else
					{
						return CharacterState.Front_Running;
					}
				}
			}
			else
			{
				if(!moving)
				{
					return CharacterState.Back_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Back_Walking;
					}
					else
					{
						return CharacterState.Back_Running;
					}
				}
			}
		}
	}

	public void ChangeCharacterState (CharacterState newState)
	{
		switch (newState)
		{
		case CharacterState.Front_Idle:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, true);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Front_Walking:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Front_Running:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		case CharacterState.Left_Idle:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, true);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Left_Walking:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Left_Running:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		case CharacterState.Right_Idle:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, false);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Right_Walking:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Right_Running:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		case CharacterState.Back_Idle:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, false);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Back_Walking:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Back_Running:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		default:
			Debug.LogWarning ("CharacterAnimator/ChangeCharacterState (CharacterState) : the character state is unknown. Not changing state.");
			break;
		}
		animator.SetTrigger (CHANGE_STATE_TRIGGER);
	}

	public void ChangeToIdle ()
	{
		moving = false;
		animator.SetTrigger (CHANGE_STATE_TRIGGER);
	}

	public enum CharacterState
	{
		Front_Idle,
		Front_Walking,
		Front_Running,
		Left_Idle,
		Left_Walking,
		Left_Running,
		Right_Idle,
		Right_Walking,
		Right_Running,
		Back_Idle,
		Back_Walking,
		Back_Running
	}

	public bool Sided
	{
		get{return sided;}
		set{sided = value;}
	}

	public bool FacingLeft
	{
		get{return facingLeft;}
		set{facingLeft = value;}
	}

	public bool FacingFront
	{
		get{return facingFront;}
		set{facingFront = value;}
	}

	public bool Moving
	{
		get{return moving;}
		set{moving = value;}
	}

	public bool Running
	{
		get{return running;}
		set{running = value;}
	}

	public CharacterState CurrentState
	{
		get{return GetCurrentState ();}
		set{ChangeCharacterState (value);}
	}
}
