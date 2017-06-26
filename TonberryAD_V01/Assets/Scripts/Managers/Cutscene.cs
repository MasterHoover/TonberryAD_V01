using UnityEngine;
using System.Collections;

[System.Serializable]
public class Cutscene 
{
	public EventPlayer.Action[] toPlay;

	public Cutscene (EventPlayer.Action[] toPlay)
	{
		this.toPlay = toPlay;
	}
}
