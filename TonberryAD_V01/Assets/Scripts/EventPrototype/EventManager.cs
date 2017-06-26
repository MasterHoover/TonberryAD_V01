using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
	private static EventManager instance;
	private LevelEvent[] levelEvents;

	public List<QueuedEvent> queuedEvents = new List<QueuedEvent> ();

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
		FetchLevelEvents ();
	}

	void FetchLevelEvents ()
	{
		levelEvents = (LevelEvent[]) GameObject.FindObjectsOfType (typeof (LevelEvent));
	}

	/*
	public void ReceiveEventStatus (string status, Event sender)
	{
		Debug.Log ("EventManager receives the event [" + status + "] from [" + sender.name);	
	}
	*/

	private void ReceiveMessage (EventMessage message)
	{
		Debug.Log ("EventManager receives the message [" + message.message + "] from [" + message.sender.name + "]");
		if (levelEvents != null)
		{
			for (int i = 0; i < levelEvents.Length; i++)
			{
				if (levelEvents[i].trigger == message.message)
				{
					LaunchEvent (levelEvents[i].launchedActions);
				}
			}
		}
	}

	void LaunchEvent (EventPlayer.OngoingEvent.ActionStep[] launchedEvent)
	{
		EventPlayer.Instance.PlayEvent (launchedEvent);
	}

	public static EventManager Instance
	{
		get{return instance;}
	}

	[System.Serializable]
	public class EventMessage
	{
		public string message;
		public MessageSender sender;

		public EventMessage (string message, MessageSender e)
		{
			this.message = message;
			this.sender = e;
		}

		public override bool Equals (object obj)
		{
			return message.Equals (((EventMessage) obj).message) && (sender != null ? sender.Equals(((EventMessage) obj).sender) : false);
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}

	[System.Serializable]
	public class QueuedEvent
	{
		public EventMessage toReceive;
		public Cutscene toLaunch;

		public QueuedEvent (EventMessage toReceive)
		{
			this.toReceive = toReceive;
		}

		public QueuedEvent (EventMessage toReceive, Cutscene toLaunch)
		{
			this.toReceive = toReceive;
			this.toLaunch = toLaunch;
		}

		public override bool Equals (object obj)
		{
			return toReceive.Equals (((QueuedEvent) obj).toReceive);
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}
}
