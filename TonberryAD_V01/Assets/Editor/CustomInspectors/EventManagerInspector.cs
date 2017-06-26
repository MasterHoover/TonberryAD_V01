using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor (typeof (EventManager))]
public class EventManagerInspector : Editor 
{
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		/*
		EventManager script = (EventManager) target;
		List<Event> queuedEvents = script.QueuedEvents;
		for (int i = 0; i < queuedEvents.Count; i++)
		{
			EditorGUILayout.TextField (i.ToString () + ")", queuedEvents[i].name);
		}
		*/
	}
}
