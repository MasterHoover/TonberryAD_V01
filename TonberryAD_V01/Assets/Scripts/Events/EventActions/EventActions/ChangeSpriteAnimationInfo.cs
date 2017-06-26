using UnityEngine;
using System.Collections;

public class ChangeSpriteAnimationInfo : SerializedClass 
{
	private string spriteName;
	private CharacterAnimator.CharacterState newState;

	public ChangeSpriteAnimationInfo (string spriteName, CharacterAnimator.CharacterState newState)
	{
		this.spriteName = spriteName;
		this.newState = newState;
	}

	public string SpriteName
	{
		get{return spriteName;}
	}

	public CharacterAnimator.CharacterState NewState
	{
		get{return newState;}
	}
}
