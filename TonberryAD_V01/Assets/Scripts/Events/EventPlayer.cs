using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventPlayer : MonoBehaviour
{
	private static EventPlayer instance;
	private List<OngoingEvent> toPlay = new List<OngoingEvent> ();
	private static int uniqueId;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
			uniqueId = 0;
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	public void PlayEvent (OngoingEvent.ActionStep[] e)
	{
		//Debug.Log ("Starting Event");
		//Debug.Log ("e Length : " + e.Length);
		if (e.Length > 0)
		{
			//Debug.Log ("aww yeah");
			toPlay.Add (new OngoingEvent (uniqueId++, e));
			StartPlaying (toPlay[toPlay.Count - 1]);
		}
	}

	public void PlayEvent (List<OngoingEvent.ActionStep> e)
	{
		PlayEvent (e.ToArray ());
	}

	private void StartPlaying (OngoingEvent toPlay)
	{
		PlayCurrentStep (toPlay);
	}

	private void OneEventDone (int uniqueId)
	{
		OneEventDone (GetOngoingEvent (uniqueId));
	}

	private void OneEventDone (OngoingEvent whichEvent)
	{
		UpdateFinished (whichEvent);
		TryNext (whichEvent);
	}

	private void TryNext (OngoingEvent whichEvent)
	{
		if (StepIsOver (whichEvent))
		{
			//Debug.Log ("Step is over");
			if (IncreaseStepIndex (whichEvent) < whichEvent.GameEvent.Length)
			{
				PlayCurrentStep (whichEvent);
			}
			else
			{
				EventIsOver (whichEvent);
			}
		}
		else
		{
			//Debug.Log ("Event done but not over");
		}
	}

	private void UpdateFinished (OngoingEvent whichEvent)
	{
		//Debug.Log ("Updating bools");
		bool foundOne = false;
		for (int i = 0; !foundOne && i < whichEvent.FinishedEvents.Length; i++)
		{
			if (!whichEvent.FinishedEvents[i])
			{
				foundOne = true;
				whichEvent.FinishedEvents[i] = true;
			}
		}
		if (!foundOne)
		{
			//Debug.Log ("Error in updating");
		}
	}

	private int GetIndexOfEvent (int uniqueID)
	{
		return toPlay.IndexOf (new OngoingEvent (uniqueID)); 
	}

	private OngoingEvent GetOngoingEvent (int uniqueID)
	{
		return toPlay[GetIndexOfEvent (uniqueID)];
	}

	private bool StepIsOver (OngoingEvent whichEvent)
	{
		bool isOver = true;
		for (int i = 0; isOver && i < whichEvent.FinishedEvents.Length; i++)
		{
			if (!whichEvent.FinishedEvents[i])
			{
				isOver = false;
			}
		}
		//stepIsOver = isOver;
		return isOver;
	}

	private int IncreaseStepIndex (OngoingEvent whichEvent)
	{
		//Debug.Log ("Going to next step : " + (o.Index + 1));
		//Debug.Log ((o.Index + 1 < o.GameEvent.Length).ToString ());
		return ++whichEvent.Index;
	}

	private void PlayStep (OngoingEvent whichEvent, int stepIndex)
	{
		//Debug.Log ("Playing current step of index " + uniqueId + " : " + stepIndex);
		whichEvent.FinishedEvents = new bool[GetNbOfEventsInCurrentStep (whichEvent)];
		int nbOfEvents = whichEvent.FinishedEvents.Length;
		//Debug.Log ("Number of events in step " + o.Index + ": " + nbOfEvents);

		if (nbOfEvents > 0)
		{
			for (int i = 0; i < whichEvent.GameEvent[stepIndex].actions.Length; i++)
			{
				if (whichEvent.GameEvent[stepIndex].actions[i] != null)
				{
					whichEvent.GameEvent[stepIndex].actions[i].PlayAction (whichEvent.UniqueID);
				}
				else
				{
					Debug.LogWarning ("EventPlayer/PlayStep (OngoingEvent, int) : an event was null : whichEventID (" + whichEvent.UniqueID + ") / Step (" + stepIndex + ")");
					OneEventDone (whichEvent);
				}
			}

			//{
				//Debug.Log ("Now playing debugLog for event at index " + uniqueId + " at step " + stepIndex);
				//DebugLog (whichEvent, whichEvent.GameEvent[stepIndex].debugLog.logMessage);
			//}
				
			// DELAY
			//if (/*!stepIsOver && */whichEvent.GameEvent[stepIndex].delay.active)
			//{
				//Debug.Log ("Now playing delay for event at index " + uniqueId + " at step " + stepIndex);
				//Debug.Log ("Duration is " + o.GameEvent[stepIndex].delay.duration);
				//Delay (whichEvent, whichEvent.GameEvent[stepIndex].delay.duration);
			//}

			//if (whichEvent.GameEvent[stepIndex].moveCharacter.active)
			//{
			//	MoveCharacter (whichEvent);
			//}

			//if (whichEvent.GameEvent[stepIndex].charAnimation.active)
			//{
			//	ChangeCharacterAnimation (whichEvent);
			//}
		}
		else
		{
			OneEventDone (whichEvent);
		}
	}

	private void PlayCurrentStep (OngoingEvent whichEvent)
	{
		PlayStep (whichEvent, whichEvent.Index);
	}

	int GetNbOfEventsInCurrentStep (OngoingEvent whichEvent)
	{
		return GetNbOfEventsInStep (whichEvent, whichEvent.Index);
	}

	int GetNbOfEventsInStep (OngoingEvent whichEvent, int stepIndex)
	{
		return whichEvent.GameEvent[stepIndex].actions.Length;
	}

	public void EventIsOver (OngoingEvent whichEvent)
	{
		Debug.Log ("Event " + whichEvent + " is over!");
		toPlay.Remove (whichEvent);
	}

	public static EventPlayer Instance
	{
		get{return instance;}
	}

	public bool PlayingEvent
	{
		get
		{
			return toPlay.Count == 0;
		}
	}

	[System.Serializable]
	public class OngoingEvent
	{
		private int uniqueId;
		public ActionStep[] gameEvent;
		private int index;
		private bool[] finishedEvents;

		public OngoingEvent (int id)
		{
			uniqueId = id;
		}

		public OngoingEvent (int uniqueId, ActionStep[] toPlay)
		{
			this.uniqueId = uniqueId;
			this.gameEvent = toPlay;
		}

		public ActionStep[] GameEvent
		{
			get{return gameEvent;}
		}

		public int Index
		{
			get{return index;}
			set{index = value;}
		}

		public bool[] FinishedEvents
		{
			get{return finishedEvents;}
			set{finishedEvents = value;}
		}

		public override bool Equals (object obj)
		{
			return uniqueId == ((OngoingEvent) obj).uniqueId;
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}

		public int UniqueID
		{
			get{return uniqueId;}
		}

		public override string ToString ()
		{
			return string.Format ("[OngoingEvent : " + uniqueId + "]");
		}

		[System.Serializable]
		public class ActionStep
		{
			public EventAction[] actions;
		}
	}

	[System.Serializable]
	public class Action
	{
		public DebugLogVars debugLog;
		public DelayVars delay;
		public MoveCharacterVars moveCharacter;
		public ChangeCharacterAnimVars charAnimation;

		[System.Serializable]
		public class DebugLogVars : Activable
		{
			public string logMessage;
		}

		[System.Serializable]
		public class DelayVars : Activable
		{
			public float duration;
		}

		[System.Serializable]
		public class MoveCharacterVars : Activable
		{
			public CharacterController toMove;
			public Transform destination;
			public MoveSpeed moveSpeed;
			public float customSpeed;

			public enum MoveSpeed
			{
				Walk,
				Run,
				CustomSpeed,
				Teleport
			}
		}

		[System.Serializable]
		public class ChangeCharacterAnimVars : Activable
		{
			public CharacterAnimator toAnimate;
			public CharacterAnimator.CharacterState state;
		}

		[System.Serializable]
		public abstract class Activable
		{
			public bool active;
		}
	}
}
