using UnityEngine;
using System.Collections;

public class TestPlayEvent : MonoBehaviour
{
	public EventPlayer.OngoingEvent.ActionStep[] firstEvent;
	public EventPlayer.OngoingEvent.ActionStep[] secondEvent;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.KeypadDivide))
		{
			EventPlayer.Instance.PlayEvent (firstEvent);
		}
		if (Input.GetKeyDown (KeyCode.KeypadMultiply))
		{
			EventPlayer.Instance.PlayEvent (secondEvent);
		}
	}
}
