using UnityEngine;
using System.Collections;

public class MoveToDestination : MonoBehaviour 
{
	private int uniqueID;
	private EventAction script;
	private Vector3 destination;
	private float speed;
	private CharacterController control;
	private bool ignoreCollisions;
	private bool ignoreHeightPosition = true;

	void Awake ()
	{
		control = GetComponent<CharacterController> ();
	}

	void Update ()
	{
		if (ignoreHeightPosition) 
		{
			destination.y = transform.position.y;
		}

		float dist = Vector3.Distance (transform.position, destination);
		Vector3 dir = (destination - transform.position).normalized;
		if (dist < speed * Time.deltaTime)
		{
			if (ignoreCollisions)
			{
				transform.Translate (dir * dist, Space.World);
			}
			else
			{
				control.Move (dir * dist);
			}
			AtDestination ();
		}
		else
		{
			if (ignoreCollisions)
			{
				transform.Translate (dir * speed * Time.deltaTime, Space.World);
			}
			else
			{
				control.Move (dir * speed * Time.deltaTime);
			}
		}
	}

	public EventAction Script
	{
		set{script = value;}
	}

	public Vector3 Destination
	{
		set{destination = value;}
	}

	public float Speed
	{
		set{speed = value;}
	}

	void AtDestination ()
	{
		script.SendMessage ("DoneMoving", this);
	}

	public int UniqueID
	{
		get{return uniqueID;}
		set{uniqueID = value;}
	}

	public bool IgnoreCollisions
	{
		get{return ignoreCollisions;}
		set{ignoreCollisions = value;}
	}

	public bool IgnoreHeightPosition
	{
		get{return ignoreHeightPosition;}
		set{ignoreHeightPosition = value;}
	}
}
