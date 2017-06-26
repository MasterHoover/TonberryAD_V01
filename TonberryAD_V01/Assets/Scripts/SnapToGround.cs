using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class SnapToGround : MonoBehaviour 
{
	public float maxSnappingDistance = 5f;
	private CharacterController charController;

	void Awake ()
	{
		if (maxSnappingDistance < 0f)
		{
			maxSnappingDistance = 0f;
			Debug.LogWarning ("SnapsBelow (" + gameObject.name + ")/Awake () : maxSnappingDistance is 0f which makes the script obsolete. " +
				"Please increase maxSnappingDistance or disable the function from the inspector.");
		}
		charController = GetComponent<CharacterController> ();
	}

	void LateUpdate ()
	{
		Snap ();
	}

	void Snap ()
	{
		if (DetectingCollisionBelow ())
		{
			charController.Move (GameManager.Instance.CardinalDown * maxSnappingDistance);
		}
	}

	bool DetectingCollisionBelow ()
	{
		Ray ray = new Ray (transform.position, Vector3.down);
		return Physics.Raycast (ray, maxSnappingDistance);
	}
}
