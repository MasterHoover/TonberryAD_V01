using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Trigger_ChangeLevel : EventTrigger 
{
	//public int levelIndex;
	public string levelName;
	public int spawnId;
	public bool useFading = true;

	protected override void LaunchEnterEvent (Collider col)
	{
		Debug.Log ("Launching Event");
		//GameManager.Instance.LoadLevel (levelIndex, spawnId, useFading);
		GameManager.Instance.LoadLevel (levelName, spawnId, useFading);
	}
}
