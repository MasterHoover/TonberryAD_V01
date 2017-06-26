using UnityEngine;
using System.Collections;

public class ChangeSpriteAnimation : EventAction 
{
	public CharacterAnimator charToAnim;
	public CharacterAnimator.CharacterState newState;

	public override SerializedClass GetSerializedInfo ()
	{
		return new ChangeSpriteAnimationInfo (charToAnim.name, newState);
	}

	public override void PlayAction (int uniqueID)
	{
		if (charToAnim != null)
		{
			charToAnim.ChangeCharacterState (newState);
			SendOverStatusToPlayer (uniqueID);
		}
		else
		{
			SendOverStatusToPlayer (uniqueID);
		}
	}

	public override void CopyInfo (SerializedClass info)
	{
		ChangeSpriteAnimationInfo i = (ChangeSpriteAnimationInfo) info;
		charToAnim = GameObject.Find (i.SpriteName).GetComponent<CharacterAnimator> ();
		newState = i.NewState;
	}
}
