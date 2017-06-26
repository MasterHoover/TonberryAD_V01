using UnityEngine;
using System.Collections;

public class ReturnToLevel : MonoBehaviour 
{
	public int level;

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			GameManager.Instance.LoadLevel (level);
		}
	}
}
