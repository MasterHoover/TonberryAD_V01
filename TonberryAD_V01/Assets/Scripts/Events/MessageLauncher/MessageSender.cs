using UnityEngine;
using System.Collections;

public class MessageSender : MonoBehaviour 
{
	protected void Ship (string message)
	{
		EventManager.Instance.SendMessage ("ReceiveMessage", new EventManager.EventMessage (message, this), SendMessageOptions.DontRequireReceiver);
	}
}
