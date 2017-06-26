using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour 
{
	protected static CutsceneManager instance;
	public CutsceneStep[] cutscene;
	private int cutsceneIndex;
	private bool queuedNext;

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

	void LateUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.KeypadMultiply))
		{
			StartCutscene ();
		}

		if (queuedNext && Input.GetKeyDown (KeyCode.Return)) 
		{
			queuedNext = false;
			StartStep (cutsceneIndex);
		}
	}

	void Start ()
	{
		FindCutsceneActorsInScene ();
	}

	void OnLevelWasLoaded (int level)
	{
		FindCutsceneActorsInScene ();
	}

	void StartCutscene ()
	{
		if (cutscene.Length > 0)
		{
			Debug.Log ("Starting cutscene!");
			cutsceneIndex = 0;
			GameManager.Instance.DisableCharacterMovement ();
			StartStep (cutsceneIndex);
		}
		else
		{
			Debug.LogWarning ("CutsceneManager(" + gameObject.name + ")/StartCutscene () : Cutscene is empty. Ignoring cutscene start.");	
		}
	}

	void FindCutsceneActorsInScene ()
	{
		CutsceneCharacter[] cutsceneActors = GameObject.FindObjectsOfType<CutsceneCharacter> ();
		for (int i = cutsceneIndex; i < cutscene.Length; i++)
		{
			CutsceneStep step = cutscene[i];
			if (step.action.action == CutsceneStep.StepAction.Action.MoveCharacter || step.action.action == CutsceneStep.StepAction.Action.RotateCharacter)
			{
				bool foundHim = false;
				for (int j = 0; !foundHim && j < cutsceneActors.Length; j++)
				{
					if (step.action.actorId == cutsceneActors[j].id)
					{
						step.action.Character = cutsceneActors[j];
						foundHim = true;
					}
				}
				if (!foundHim)
				{
					Debug.LogWarning ("CutsceneManager(" + gameObject.name + ")/RegisterCutsceneActors () : Didn't find cutsceneCharacter for stepIndex " + i);
				}
			}
		}
	}

	void StartStep (int index)
	{
		cutscene[index].action.DoAction ();
	}

	void StepOver ()
	{
		Debug.Log ("Step is over!");
		if (!Next ())
		{
			Debug.Log ("Cutscene is over!");
			GameManager.Instance.EnableCharacterMovement ();
		}
		else
		{
			queuedNext = true;
		}
	}

	bool Next ()
	{
		return ++cutsceneIndex < cutscene.Length;
	}

	public static CutsceneManager Instance
	{
		get{return instance;}
	}

	[System.Serializable]
	public class CutsceneStep
	{
		public StepAction action;

		[System.Serializable]
		public class StepAction
		{
			public enum Action
			{
				StartDialog,
				MoveCharacter,
				RotateCharacter,
				Delay,
				FadeIn,
				FadeOut,
				FadeInInstant
			}

			public Action action;
			public Quote[] dialog;
			public int actorId;
			public Transform moveTarget;
			public float delayDuration;
			public bool rotateLeft;
			public float rotationDegree;
			private CutsceneCharacter character;

			public void DoAction ()
			{
				switch (action)
				{
				case Action.StartDialog:
					Debug.Log ("StartingDialog!");
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				case Action.MoveCharacter:
					Debug.Log ("MovingCharacter : " + character.charName);
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				case Action.RotateCharacter:
					Debug.Log ("Rotating character : " + character.charName);
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				case Action.Delay:
					Debug.Log ("Doing delay!");
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				case Action.FadeIn:
					Debug.Log ("Fading in!");
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				case Action.FadeOut:
					Debug.Log ("Fading Out!");
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				case Action.FadeInInstant:
					Debug.Log ("Doing instant fade in!");
					CutsceneManager.Instance.SendMessage ("StepOver");
					break;
				}
			}

			public CutsceneCharacter Character
			{
				get{return character;}
				set{character = value;}
			}
		}
	}
}
