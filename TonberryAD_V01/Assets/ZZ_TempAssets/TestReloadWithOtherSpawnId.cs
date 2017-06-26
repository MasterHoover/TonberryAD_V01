using UnityEngine;
using System.Collections;

public class TestReloadWithOtherSpawnId : MonoBehaviour 
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Keypad0))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 0, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad1))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 1, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad2))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 2, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad3))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 3, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad4))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 4, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad5))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 5, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad6))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 6, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad7))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 7, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad8))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 8, true);
		}
		else if (Input.GetKeyDown (KeyCode.Keypad9))
		{
			GameManager.Instance.LoadLevel (GameManager.Instance.CurrentLevelName, 9, true);
		}
	}
}
