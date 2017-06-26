using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public abstract class EventTrigger : MonoBehaviour 
{
	public string[] agentsTag = new string[1];
	protected virtual void LaunchEnterEvent (Collider col) {}
	protected virtual void LaunchExitEvent (Collider col) {}

	void Awake ()
	{
		GetComponent<BoxCollider> ().isTrigger = true;
		tag = "EventTrigger";
	}
		
	void OnCollisionEnter (Collision col)
	{
		for (int i = 0; i < agentsTag.Length; i++)
		{
			if (col.collider.tag == agentsTag[i])
			{
				LaunchEnterEvent (col.collider);
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		for (int i = 0; i < agentsTag.Length; i++)
		{
			if (col.tag == agentsTag[i])
			{
				LaunchEnterEvent (col);
			}
		}
	}
		
	void OnCollisionExit (Collision col)
	{
		for (int i = 0; i < agentsTag.Length; i++)
		{
			if (col.collider.tag == agentsTag[i])
			{
				LaunchExitEvent (col.collider);
			}
		}
	}

	void OnTriggerExit (Collider col)
	{
		for (int i = 0; i < agentsTag.Length; i++)
		{
			if (col.tag == agentsTag[i])
			{
				LaunchExitEvent (col);
			}
		}
	}
}
