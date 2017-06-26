using UnityEngine;
using System.Collections;

public class MessageReceiver : MonoBehaviour
{
	public void Catch (string message)
	{
		SendMessage ("Receive", message);
	}
}
