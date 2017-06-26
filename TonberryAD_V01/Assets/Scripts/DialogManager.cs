using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour 
{
	public DialogCanvas dialogCanvas;
	private static DialogManager instance;
	private int dialogIndex;
	private Quote[] quotes;
	private DialogCanvas dialogCanvasInstance;
	private bool playingDialog;
	private bool canvasInScene;

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

	void Start ()
	{
		canvasInScene = FindDialogCanvas ();
		if (!canvasInScene)
		{
			dialogCanvasInstance = SpawnCanvas ();
			canvasInScene = dialogCanvasInstance != null;
		}
		if (canvasInScene)
		{
			HideDialogCanvas ();
		}
		else
		{
			Debug.LogWarning ("DialogManager/Start () : Coudn't find or spawn dialogCanvas in scene");
		}
	}

	void OnLevelWasLoaded ()
	{
		if (dialogCanvasInstance == null)
		{
			canvasInScene = FindDialogCanvas ();
			if (dialogCanvasInstance == null)
			{
				dialogCanvasInstance = SpawnCanvas ();
				canvasInScene = dialogCanvasInstance != null;
			}
			if (canvasInScene)
			{
				HideDialogCanvas ();
			}
			else
			{
				Debug.LogWarning ("DialogManager/Start () : Coudn't find or spawn dialogCanvas in scene");
			}
		}
	}

	void Update ()
	{
		if (playingDialog && Input.GetKeyDown (KeyCode.Return))
		{
			if (Next ())
			{
				PlayQuote (dialogIndex);
			}
			else
			{
				playingDialog = false;
				HideDialogCanvas ();
				GameManager.Instance.EnableCharacterMovement ();
				GameManager.Instance.SendMessage ("DialogEnded");
			}
		}
	}

	public static DialogManager Instance
	{
		get{return instance;}
	}
		
	public void StartDialog (Quote[] quotes)
	{
		StartDialog (quotes, null);
	}

	public void StartDialog (Quote[] quotes, Character secondSpeaker)
	{
		if (canvasInScene)
		{
			this.quotes = new Quote[quotes.Length];
			this.quotes = quotes;
			FillDefaultCharacters (ref this.quotes, secondSpeaker);
			dialogIndex = 0;
			if (this.quotes.Length > 0)
			{
				playingDialog = true;
				ShowDialogCanvas ();
				GameManager.Instance.DisableCharacterMovement ();
				PlayQuote (dialogIndex);
			}
			else
			{
				Debug.LogWarning ("DialogManager/PlayDialog (Quote[]) : dialog has no quotes. Aborting start.");
			}
		}
		else
		{
			Debug.LogWarning ("DialogManager/StartDialog : Trying to start a dialog, but there's no DialogCanvas in scene.");
		}
	}

	private bool Next ()
	{
		if (++dialogIndex < quotes.Length)
		{
			return true;
		}
		return false;
	}

	private void PlayQuote (int index)
	{
		string nameText = "";
		string dialogText = "";
		if (quotes[index].talkingCharacter != null)
		{
			nameText = quotes[index].talkingCharacter.charName;
		}
		else
		{
			Debug.LogWarning ("DialogManager/PlayQuote (int) : character is null at index " + index + ". Name will be null.");
		}
		dialogText = quotes[index].dialog;
		dialogCanvasInstance.nameText.text = nameText;
		while (dialogIndex + 1 < quotes.Length && quotes[dialogIndex + 1].append && Next ())
		{
			Append (dialogIndex, ref dialogText);
		}
		dialogCanvasInstance.dialogText.text = dialogText;
	}

	private void Append (int index, ref string text)
	{
		text += ("\n" + quotes[dialogIndex].dialog);
	}

	private bool FindDialogCanvas ()
	{
		GameObject canvasObj = GameObject.FindGameObjectWithTag ("DialogCanvas");
		if (canvasObj != null)
		{
			dialogCanvasInstance = canvasObj.GetComponent <DialogCanvas> ();
			return true;
		}
		else
		{
			return false;
		}
	}

	private void HideDialogCanvas ()
	{
		dialogCanvasInstance.gameObject.SetActive (false);
	}

	private void ShowDialogCanvas ()
	{
		dialogCanvasInstance.gameObject.SetActive (true);
	}

	private DialogCanvas SpawnCanvas ()
	{
		if (dialogCanvas != null)
		{
			return dialogCanvasInstance = (DialogCanvas) Instantiate (dialogCanvas, Vector3.zero, Quaternion.identity);
		}
		else
		{
			Debug.LogWarning ("DialogManager/SpawnCanvas () : no prefab of dialogCanvas is assigned. Not spawning Canvas");
			return null;
		}
	}

	void FillDefaultCharacters (ref Quote[] dialog, Character secondSpeaker)
	{
		if (GameManager.Instance.Player != null)
		{
			for (int i = 0; i < dialog.Length; i++)
			{
				if (dialog[i].player)
				{
					dialog[i].talkingCharacter = GameManager.Instance.Player.controlledCharacter;
				}
				else if (dialog[i].talkingCharacter == null)
				{
					dialog[i].talkingCharacter = secondSpeaker;
				}
			}
		}
	}
}

[System.Serializable]
public class Quote 
{
	public bool player;
	public bool append;
	public Character talkingCharacter;
	public string dialog;
}
