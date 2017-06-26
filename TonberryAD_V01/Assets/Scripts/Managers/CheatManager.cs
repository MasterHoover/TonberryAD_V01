using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour 
{
	public bool escapeLoadWorldMap = true;

	void Update ()
	{
		if (escapeLoadWorldMap && Input.GetKeyDown (KeyCode.Escape))
		{
			GameManager.Instance.LoadWorldMap (SceneManager.GetActiveScene ().name);
		}
	}
}
