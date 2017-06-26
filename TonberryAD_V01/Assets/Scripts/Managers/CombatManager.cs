using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour 
{
	private static CombatManager instance;
	private int navigationLevel;
	private const string COMBAT_SCENE_NAME = "CombatScene";

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void OnLevelWasLoaded (int level)
	{
		if (GameManager.Instance.CurrentLevelName != COMBAT_SCENE_NAME)
		{
			navigationLevel = level;
		}
	}

	public void StartCombat ()
	{
		GameManager.Instance.LoadLevel (COMBAT_SCENE_NAME, true);
	}

	public void EndCombat ()
	{
		GameManager.Instance.LoadLevel (navigationLevel, true);
	}

	public static CombatManager Instance
	{
		get{return instance;}
	}
}
