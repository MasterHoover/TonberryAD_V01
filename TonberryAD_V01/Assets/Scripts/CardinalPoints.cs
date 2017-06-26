using UnityEngine;
using System.Collections;

public class CardinalPoints : MonoBehaviour 
{
	public int id;

	public Vector3 North
	{
		get{return transform.forward;}
	}

	public Vector3 East
	{
		get{return transform.right;}
	}

	public Vector3 West
	{
		get{return -transform.right;}
	}

	public Vector3 South
	{
		get{return -transform.forward;}
	}

	public Vector3 Up
	{
		get{return transform.up;}
	}

	public Vector3 Down
	{
		get{return -transform.up;}
	}
}
