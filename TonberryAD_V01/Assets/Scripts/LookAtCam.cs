
using UnityEngine;
using System.Collections;

public class LookAtCam : MonoBehaviour 
{
	void Start ()
	{
		LookCamera ();
	}

	void LateUpdate ()
	{
		LookCamera ();
	}

	public void LookCamera ()
	{
		transform.LookAt (transform.position + Camera.main.transform.forward);
	}
}
