using UnityEngine;
using System.Collections;

public class EventLauncher : MonoBehaviour 
{
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "EventTrigger")
		{
			//col.GetComponent <EventTrigger> ().LaunchEvent ();
		}
	}
}
