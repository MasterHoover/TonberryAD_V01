using UnityEngine;
using System.Collections;

public abstract class CustomGizmos : MonoBehaviour 
{
	public bool showGizmos = true;
	public bool selectedOnly;

	void OnDrawGizmos ()
	{
		if(showGizmos && !selectedOnly)
		{
			DrawGizmos ();
		}
	}

	void OnDrawGizmosSelected ()
	{
		if(showGizmos && selectedOnly)
		{
			DrawGizmos ();
		}
	}

	protected abstract void DrawGizmos ();
}
