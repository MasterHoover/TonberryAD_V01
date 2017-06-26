using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour 
{
	public GameObject cursor;
	public MapArea[] areas;

	private int selectionIndex;
	private bool buttonDown;
	private bool up;

	void Start ()
	{
		int previousLevel = GetSelectionIndex (GameManager.Instance.PreviousLevel);
		if (previousLevel != -1)
		{
			SetSelection (previousLevel);
		}

		if (areas.Length > 0)
		{
			ShowSelection ();
		}
		else
		{
			enabled = false;
		}
	}

	void Update ()
	{
		if (!buttonDown)
		{
			if (Input.GetAxisRaw ("Vertical") > 0f)
			{
				buttonDown = true;
				while (Previous () == null){}
				ShowSelection ();
			}
			else
			{
				if (Input.GetAxisRaw ("Vertical") < 0f)
				{
					buttonDown = true;
					while (Next () == null){}

					ShowSelection ();
				}
			}

		}
		else
		{
			if (Input.GetAxisRaw ("Vertical") == 0f)
			{
				buttonDown = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.Return))
		{
			ConfirmSelection ();
		}
	}

	private MapArea Previous ()
	{
		if (selectionIndex - 1 < 0)
		{
			selectionIndex = areas.Length - 1;
		}
		else
		{
			selectionIndex--;
		}
		return areas[selectionIndex];
	}

	private MapArea Next ()
	{
		if (selectionIndex + 1 == areas.Length)
		{
			selectionIndex = 0;
		}
		else
		{
			selectionIndex++;
		}
		return areas[selectionIndex];
	}

	void ShowSelection ()
	{
		if (areas[selectionIndex] != null)
		{
			Debug.Log ("Selection is : " + areas[selectionIndex].area);
		}
	}

	void ConfirmSelection ()
	{
		GameManager.Instance.LoadLevel (areas[selectionIndex].levelToLoad, areas[selectionIndex].spawnId, true);
	}

	void SetSelection (int index)
	{
		selectionIndex = index;
	}
		
	int GetSelectionIndex (string levelToLoad)
	{
		for (int i = 0; i < areas.Length; i++)
		{
			if (areas[i] != null && areas[i].levelToLoad == levelToLoad)
			{
				return i;
			}
		}
		return -1;
	}

	public enum Map
	{
		Lidiko,
		Forest,
		Montblanc,
		Capitale,
		OldAcademy,
		VillePortuaire,
		Tower
	}
}
