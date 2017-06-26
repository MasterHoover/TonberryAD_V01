using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	void Update ()
	{
		if(Input.GetKeyDown (KeyCode.Return))
		{
			GameManager.Instance.LoadNextLevel (true);
		}
	}
}
