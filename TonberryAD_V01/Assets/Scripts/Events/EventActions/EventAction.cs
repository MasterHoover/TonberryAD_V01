using UnityEngine;
using System.Collections;

public abstract class EventAction : MonoBehaviour, ISerializable<SerializedClass>
{
	public abstract SerializedClass GetSerializedInfo ();
	public abstract void CopyInfo (SerializedClass info);
	public virtual void PlayAction (int uniqueID)
	{
		SendOverStatusToPlayer (uniqueID);
	}

	public void SendOverStatusToPlayer (int uniqueID)
	{
		EventPlayer.Instance.SendMessage ("OneEventDone", uniqueID);
	}
}
