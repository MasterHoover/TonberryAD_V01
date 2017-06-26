using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (FadeManager))]
[RequireComponent (typeof (CameraManager))]
[RequireComponent (typeof (EventManager))]
[RequireComponent (typeof (EventPlayer))]
[RequireComponent (typeof (MessageSender))]
[RequireComponent (typeof (DialogManager))]
[RequireComponent (typeof (CombatManager))]
public class GameManager : MonoBehaviour, IMessageReceiver
{
	private static GameManager instance;
	public ControllableCharacter playerCharacterPrefab;
	private ControllableCharacter playerCharacterInstance;
	private int spawnId;
	private string previousLevel;
	private Transform cardinalPoints;
	private Transform queuedCardinals;
	private bool queuedChangeCardinals;
	private Quaternion newCardinalRot;
	private GameState gameState = new GameState ();
	private CameraManager camTraveler;
	private LevelState currentLevelState;

	void Awake ()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			DestroyImmediate (gameObject);
		}

		if (playerCharacterPrefab == null)
		{
			Debug.LogWarning ("GameManager/Awake () : No playerCharacter prefab is assigned. Disabling script.");
			enabled = false;
		}
	}

	public void Receive (string message)
	{
		Debug.Log (gameObject.name + " received message : " + message);
	}

	void Start ()
	{
		if (FadeManager.Instance == null)
		{
			Debug.LogWarning ("GameManager/Start () : There's no FadeManager in project");
		}

		Refresh ();

		Debug.Log ("Loaded Level : " + SceneManager.GetActiveScene ().name);
		ShipMessage ("LevelLoaded");
	}

	void OnLevelWasLoaded (int level)
	{
		if(FadeManager.Instance != null)
		{
			FadeManager.Instance.RefreshCamOverlay ();
		}

		/*
		if (queueLoadLevel)
		{
			queueLoadLevel = false;
			if (FadeManager.Instance != null)
			{
				FadeManager.Instance.FadeOut ();
			}
		}
		*/

		switch (level)
		{
		case 1 : 
			FadeManager.Instance.FadeOut (1f, 1f);
			break;
		}

		Refresh ();
		Debug.Log ("Loaded Level : " + SceneManager.GetActiveScene ().name);
		ShipMessage ("LevelLoaded");
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();	
		}

		if (queuedChangeCardinals && Input.GetAxisRaw ("Horizontal") == 0f && Input.GetAxisRaw ("Vertical") == 0f)
		{
			queuedChangeCardinals = false;
			cardinalPoints = queuedCardinals;
		}
	}

	void Refresh ()
	{
		FetchLevelStateAndOverride ();
		if (Player == null)
		{
			PlayerSpawn spawnInfo = FindPlayerSpawn (spawnId);
			if (spawnInfo != null)
			{
				SpawnPlayerCharacter (spawnInfo.transform);
				CameraManager.Instance.ChangeCameraView (spawnInfo.viewInfo);
			}
			else
			{
				Debug.Log ("GameManager/Refresh () : No spawnInfo was found.");
			}
		}
	}
		
	void ShipMessage (string message)
	{
		GetComponent<MessageSender> ().SendMessage ("Ship", message);
	}

	void SpawnPlayerCharacter (Transform spawnPosReference)
	{
		SpawnPlayerCharacter (spawnPosReference.position, spawnPosReference.rotation);
	}

	void SpawnPlayerCharacter (Vector3 position, Quaternion rotation)
	{
		if (playerCharacterPrefab != null)
		{
			playerCharacterInstance = (ControllableCharacter) Instantiate (playerCharacterPrefab, position, rotation);
			playerCharacterInstance.Direction = playerCharacterInstance.transform.forward;
		}
		else
		{
			Debug.LogWarning ("GameManager/SpawnPlayerCharacter (Vector3, Quaternion) : No playerCharacterPrefab is assigned. Not Spawning PlayerCharacter.");
		}
	}

	PlayerSpawn FindPlayerSpawn (int id)
	{
		PlayerSpawn[] playerSpawns = GameObject.FindObjectsOfType<PlayerSpawn> ();
		for (int i = 0; i < playerSpawns.Length; i++)
		{
			if (playerSpawns[i].id == spawnId)
			{
				return playerSpawns[i];
			}
		}
		Debug.LogWarning ("GameManager/SpawnPlayerCharacter () : Didn't find any SpawnInfo with id : " + id);
		return null;
	}
		
	public void LoadNextLevel ()
	{
		Debug.Log ("LoadNext");
		LoadNextLevel (false);
	}

	public void LoadNextLevel (bool withFading)
	{
		LoadLevel (SceneManager.GetActiveScene ().buildIndex + 1, withFading); 
	}
		
	public void LoadLevel (int level)
	{
		LoadLevel (level, spawnId, false);
	}
		
	public void LoadLevel (int level, bool withFading)
	{
		LoadLevel (level, spawnId, withFading);	
	}

	public void LoadLevel (int level, int id)
	{
		LoadLevel (level, id, false);
	}

	public void LoadLevel (int level, int id, bool withFading)
	{
		SaveCurrentLevelState ();
		spawnId = id;
		if(withFading)
		{
			/*
			queueLoadLevel = true;
			queuedLevel = level;
			FadeManager.Instance.FadeIn ();
			*/
			FadeManager.Instance.FadeInAndChangeLevel (level);
			FadeManager.Instance.FadeOutAfterChangingLevel ();
		}
		else
		{
			SceneManager.LoadScene (level);
		}
	}

	public void LoadLevel (string levelName, int id, bool withFading)
	{
		SaveCurrentLevelState ();
		spawnId = id;
		if (withFading)
		{
			FadeManager.Instance.FadeInAndChangeLevel (levelName);
			FadeManager.Instance.FadeOutAfterChangingLevel ();
		}
		else
		{
			SceneManager.LoadScene (levelName);
		}
	}

	public void LoadLevel (string levelName, int id)
	{
		LoadLevel (levelName, id, false);
	}

	public void LoadLevel (string levelName, bool withFading)
	{
		LoadLevel (levelName, spawnId, withFading);
	}

	public void LoadLevel (string levelName)
	{
		LoadLevel (levelName, spawnId, false);
	}

	public void LoadWorldMap (string previousLevel)
	{
		this.previousLevel = previousLevel;
		LoadLevel ("WorldMap", true);
	}

	public void TeleportScene (Transform charPos, Transform camPos)
	{
		playerCharacterInstance.transform.position = charPos.position;
		Camera.main.transform.position = camPos.position;
		Camera.main.transform.rotation = camPos.rotation;
		playerCharacterInstance.GetComponent<LookAtCam> ().LookCamera ();
	}

	public void EnableCharacterMovement ()
	{
		if (playerCharacterInstance != null)
		{
			playerCharacterInstance.EnableCharacter ();
		}
	}

	public void DisableCharacterMovement ()
	{
		if (playerCharacterInstance != null)
		{
			playerCharacterInstance.DisableCharacter ();
		}
	}

	public void FadeOver ()
	{
		/*
		if (queueLoadLevel)
		{
			LoadLevel (queuedLevel, spawnId);
		}
		*/
	}

	public void DialogEnded ()
	{
		Debug.Log ("Dialog Ended!");
		EnableCharacterMovement ();
	}

	public void ChangeCardinals (Transform reference)
	{
		if (reference != null)
		{
			queuedChangeCardinals = true;
			queuedCardinals = reference;
		}
		else
		{
			Debug.LogWarning ("GameManager/ChangeCardinals(Transform) : reference is null.");
		}
	}

	public void FetchLevelStateAndOverride ()
	{
		currentLevelState = (LevelState) GameObject.FindObjectOfType <LevelState> ();
		RestoreLevelState ();
	}

	public void RestoreLevelState ()
	{
		if (currentLevelState != null)
		{
			int index = gameState.LevelStates.IndexOf (currentLevelState.GetSerializedInfo ());
			if (index != -1)
			{
				// Found the levelState, restoring using this one.
				currentLevelState.Copy (gameState.LevelStates[index].ModifierStates);
			}
			else
			{
				// Didn't find the level state. Will look into the Unknown versions
				Debug.LogWarning ("GameManager/OverrideLevelStateInScene () : There was no saved LevelState in manager to restore.");
				Debug.Log ("Trying Unknown");
				int indexUnknown = gameState.UnknownLevelStateInfo.IndexOf (new UnknownLevelStateInfo (CurrentLevelName));
				if (indexUnknown != -1)
				{
					Debug.Log ("Found Unknown mod of currentLevel");
					// Level has been registered into unknown. Will restore all its modifiers.
					while (gameState.UnknownLevelStateInfo[indexUnknown].UnknownLevelModifiers.Count > 0)
					{
						currentLevelState.Copy (gameState.UnknownLevelStateInfo[indexUnknown].UnknownLevelModifiers[0].ModifierName, 
							gameState.UnknownLevelStateInfo[indexUnknown].UnknownLevelModifiers[0].ActivationCount);
						gameState.UnknownLevelStateInfo[indexUnknown].UnknownLevelModifiers.RemoveAt (0);
					}
					gameState.UnknownLevelStateInfo.RemoveAt (indexUnknown);
				}
				else
				{
					Debug.LogWarning ("GameManager/OverrideLevelStateInScene () : There was no saved UnknownLevelStateInfo to restore.");
				}

			}
		}
		else
		{
			Debug.LogWarning ("GameManager/OverrideLevelStateInScene () : There is no levelState in scene.");
		}
	}

	public void OverrideSavedLevelState (LevelStateInfo info)
	{
		int index = gameState.LevelStates.IndexOf (info);
		if (index != -1)
		{
			gameState.LevelStates[index] = info;
		}
		else
		{
			gameState.LevelStates.Add (info);
		}
	}
		
	public void ChangeLevelState (LevelStateChangeInfo changeInfo)
	{
		int levelStateInfoIndex = gameState.LevelStates.IndexOf (new LevelStateInfo (changeInfo.LevelName));
		if (levelStateInfoIndex != -1)
		{
			gameState.LevelStates[levelStateInfoIndex].ChangeState (changeInfo.ModifierName, changeInfo.Action);
		}
		else
		{
			if (changeInfo.LevelName != CurrentLevelName)
			{
				int unknownLevelStateInfoIndex = gameState.UnknownLevelStateInfo.IndexOf (new UnknownLevelStateInfo (changeInfo.LevelName));
				if (unknownLevelStateInfoIndex == -1) // If new level hasn't been registered
				{
					gameState.UnknownLevelStateInfo.Add (new UnknownLevelStateInfo (changeInfo.LevelName, changeInfo.ModifierName));
					int indexOfAddedItem = gameState.UnknownLevelStateInfo.Count - 1;
					gameState.UnknownLevelStateInfo[indexOfAddedItem].ChangeState (changeInfo.ModifierName, changeInfo.Action);
					Debug.LogWarning ("GameManager/ChangeLevelState (LevelStateChangeInfo change) : level of levelChange isn't registered.");
				}
				else 
				{
					// The level is already registered in UnknownLevelStateInfo
					// Look if the modifier has been registered in this level
					int unknownModIndex = gameState.UnknownLevelStateInfo[unknownLevelStateInfoIndex].
						UnknownLevelModifiers.IndexOf (new LevelState.UnknownLevelModifier (changeInfo.ModifierName));
					if (unknownModIndex == -1) 
					{
						// Modifier hasn't been registered. Will add that modifier.
						gameState.UnknownLevelStateInfo[unknownLevelStateInfoIndex].UnknownLevelModifiers.Add (new LevelState.UnknownLevelModifier (changeInfo.ModifierName));
						// Then gotta Trigger/Untrigger that modifier
					}
					gameState.UnknownLevelStateInfo[unknownLevelStateInfoIndex].ChangeState (changeInfo.ModifierName, changeInfo.Action);
				}
			}
		}

		if (changeInfo.LevelName == CurrentLevelName)
		{
			currentLevelState.ChangeState (changeInfo.ModifierName, changeInfo.Action);
		}
	}

	public void SaveCurrentLevelState ()
	{
		if (currentLevelState != null)
		{
			OverrideSavedLevelState (currentLevelState.GetSerializedInfo ());
		}
	}

	public static GameManager Instance
	{
		get{return instance;}
	}

	public ControllableCharacter Player
	{
		get{return playerCharacterInstance;}
	}

	public Vector3 CardinalNorth
	{
		get
		{
			return cardinalPoints != null ? cardinalPoints.forward : Vector3.forward;
		}
	}

	public Vector3 CardinalEast
	{
		get
		{
			return cardinalPoints != null ? cardinalPoints.right : Vector3.right;
		}
	}

	public Vector3 CardinalWest
	{
		get
		{
			return cardinalPoints != null ? -cardinalPoints.right : Vector3.left;
		}
	}

	public Vector3 CardinalSouth
	{
		get
		{
			return cardinalPoints != null ? -cardinalPoints.forward : Vector3.back;
		}
	}

	public Vector3 CardinalUp
	{
		get
		{
			return cardinalPoints != null ? cardinalPoints.up : Vector3.up;
		}
	}

	public Vector3 CardinalDown
	{
		get
		{
			return cardinalPoints != null ? -cardinalPoints.up : Vector3.down;
		}
	}

	public string PreviousLevel
	{
		get{return previousLevel;}
	}

	public Transform CardinalPoints
	{
		get{return cardinalPoints;}
	}

	public Quaternion LastCardinalRot
	{
		get
		{
			return queuedChangeCardinals ? newCardinalRot : cardinalPoints.transform.rotation;
		}
	}

	public string CurrentLevelName
	{
		get{return SceneManager.GetActiveScene ().name;}
	}

	public int CurrentLevelIndex
	{
		get{return SceneManager.GetActiveScene ().buildIndex;}
	}

	[System.Serializable]
	public class GameState
	{
		private List<LevelStateInfo> levelStates = new List<LevelStateInfo> ();
		private List <UnknownLevelStateInfo> unknownLevelModifiers = new List<UnknownLevelStateInfo> ();

		public List<LevelStateInfo> LevelStates
		{
			get{return levelStates;}
		}

		public List<UnknownLevelStateInfo> UnknownLevelStateInfo
		{
			get{return unknownLevelModifiers;}
		}
	}
}
