using UnityEngine;
using System.Collections;

public abstract class InputAction : MonoBehaviour 
{
	public string inputName;

	void Update ()
	{
		if (Input.GetButtonDown (inputName))
		{
			ButtonDownAction ();
		}
		if (Input.GetButtonUp (inputName))
		{
			ButtonUpAction ();
		}
		if (Input.GetButton (inputName))
		{
			ButtonAction ();
		}
		else
		{
			NotButtonAction ();
		}
	}

	protected virtual void ButtonDownAction (){}
	protected virtual void ButtonAction (){}
	protected virtual void ButtonUpAction (){}
	protected virtual void NotButtonAction (){}
	public virtual void ForceExit (){}
}
