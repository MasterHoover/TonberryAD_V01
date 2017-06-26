using UnityEngine;
using System.Collections;

public class MoveCharacter : EventAction 
{
	public Transform charToMove;
	public Transform destination;
	public MoveSpeed moveSpeed;
	public float customSpeed;
	public bool useMoveSpeedAnimation = true;
	public CharacterAnimator.CharacterState anim;
	private CharacterAnimator charAnimator;
	public bool ignoreCollisions;
	public bool ignoreHeightPosition = true;

	void Awake ()
	{
		if (charToMove != null)
		{
			charAnimator = charToMove.GetComponent<CharacterAnimator> ();
		}
		else
		{
			enabled = false;
		}
	}

	public override SerializedClass GetSerializedInfo ()
	{
		return new MoveCharacterInfo (charToMove.name, destination.position, moveSpeed, customSpeed, useMoveSpeedAnimation, anim);
	}

	public override void PlayAction (int uniqueID)
	{
		if (charToMove != null)
		{
			MoveToDestination existentScript = charToMove.GetComponent<MoveToDestination> ();
			if (existentScript == null)
			{
				CharacterController charCont = charToMove.GetComponent<CharacterController> ();
				SnapToGround snap = charToMove.GetComponent<SnapToGround> ();

				if (ignoreCollisions)
				{
					if (snap != null && snap.enabled)
					{
						snap.enabled = false;
					}
				}
				else
				{
					if (charCont == null)
					{
						charToMove.gameObject.AddComponent<CharacterController> ();
					}
					if (snap == null)
					{
						charToMove.gameObject.AddComponent<SnapToGround> ();
					}
					if (!snap.enabled)
					{
						snap.enabled = true;
					}
				}

				if (moveSpeed == MoveSpeed.Teleport)
				{
					Debug.Log ("TELEPORTING");
					charToMove.transform.position = destination.position;
					SendOverStatusToPlayer (uniqueID);
				}
				else
				{
					if (ignoreCollisions)
					{
						
						if (snap != null)
						{
							snap.enabled = false;
						}
					}


					MoveToDestination script = charToMove.gameObject.AddComponent<MoveToDestination> ();
					script.UniqueID = uniqueID;
					script.Script = this;
					script.Destination = destination.position;
					script.Speed = 
						moveSpeed == MoveSpeed.Walk ? ControllableCharacter.DEFAULT_WALKING_SPEED : 
						moveSpeed == MoveSpeed.Run ? Run.DEFAULT_RUNNING_SPEED : 
						customSpeed;
					script.IgnoreCollisions = ignoreCollisions;
					script.IgnoreHeightPosition = ignoreHeightPosition;

					if (charAnimator != null)
					{
						if (useMoveSpeedAnimation)
						{
							charAnimator.UpdateCharacterParams ((destination.position - charToMove.transform.position).normalized, true, 
								moveSpeed == MoveSpeed.Run || moveSpeed == MoveSpeed.Custom && customSpeed >= Run.DEFAULT_RUNNING_SPEED);
						}
						else
						{
							charAnimator.ChangeCharacterState (anim);
						}
					}
				}
			}
			else
			{
				SendOverStatusToPlayer (uniqueID);
			}
		}
		else
		{
			SendOverStatusToPlayer (uniqueID);
		}
	}

	public override void CopyInfo (SerializedClass info)
	{
		MoveCharacterInfo i = (MoveCharacterInfo) info;
		charToMove = GameObject.Find (i.CharName).transform;
		destination.position = i.Destination;
		moveSpeed = i.MoveSpeed;
		customSpeed = i.CustomSpeed;
	}

	private void DoneMoving (MoveToDestination script)
	{
		if (charAnimator != null && useMoveSpeedAnimation)
		{
			charAnimator.ChangeToIdle ();
		}
		int uniqueID = script.UniqueID;
		DestroyImmediate (script);
		SendOverStatusToPlayer (uniqueID);
	}

	public enum MoveSpeed
	{
		Walk,
		Run,
		Custom,
		Teleport
	}

	public CharacterAnimator CharAnimator
	{
		get{return charAnimator;}
	}
}
