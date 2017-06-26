using UnityEngine;
using System.Collections;

public class Test_LaunchDialog : MonoBehaviour 
{
	public Quote[] dialog;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.KeypadMinus))
		{
			DialogManager.Instance.StartDialog (dialog);
		}
	}
}
