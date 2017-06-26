using UnityEngine;
using System.Collections;

public class NPC : Character, IInteractable
{
	public Quote[] dialog;

	void Awake ()
	{
		tag = "Interactable";
	}

	public void Interact ()
	{
		DialogManager.Instance.StartDialog (dialog, this);
	}
}
