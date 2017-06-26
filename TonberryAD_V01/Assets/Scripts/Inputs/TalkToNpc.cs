using UnityEngine;
using System.Collections;

public class TalkToNpc : InputAction
{
	public LayerMask ignoreForDialogRaycast;
	public float raycastLength = 0.3f;
	public float raycastOffsetDist = 0.5f;

	protected override void ButtonDownAction ()
	{
		Ray ray = new Ray (transform.position - transform.forward * raycastOffsetDist, transform.forward * (raycastLength + raycastOffsetDist));
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, raycastLength + raycastOffsetDist, 1 << ignoreForDialogRaycast))
		{
			if (hit.collider.tag == "Interactable")
			{
				hit.collider.SendMessage ("Interact", this);
			}
			else
			{
				Debug.Log (hit.collider.name);
			}
		}
		else
		{
			//Debug.Log ("Fail");
		}
	}
}
