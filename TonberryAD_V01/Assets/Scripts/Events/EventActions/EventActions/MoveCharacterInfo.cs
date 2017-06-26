using UnityEngine;
using System.Collections;

public class MoveCharacterInfo : SerializedClass 
{
	private string charName;
	private Vector3 destination;
	private MoveCharacter.MoveSpeed moveSpeed;
	private float customSpeed;
	private bool useDefaultAnimation = true;
	private CharacterAnimator.CharacterState anim;

	public MoveCharacterInfo (string charName, Vector3 destination, MoveCharacter.MoveSpeed moveSpeed, float customSpeed, bool useDefaultAnimation, CharacterAnimator.CharacterState anim)
	{
		this.charName = charName;
		this.destination = destination;
		this.moveSpeed = moveSpeed;
		this.customSpeed = customSpeed;
		this.useDefaultAnimation = useDefaultAnimation;
		this.anim = anim;
	}

	public string CharName
	{
		get{return charName;}
	}

	public Vector3 Destination
	{
		get{return destination;}
	}

	public MoveCharacter.MoveSpeed MoveSpeed
	{
		get{return moveSpeed;}
	}

	public float CustomSpeed
	{
		get{return customSpeed;}
	}

	public bool UseDefaultAnimation
	{
		get{return useDefaultAnimation;}
	}

	public CharacterAnimator.CharacterState Anim
	{
		get{return anim;}
	}
}
