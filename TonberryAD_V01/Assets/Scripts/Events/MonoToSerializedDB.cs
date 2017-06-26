using UnityEngine;
using System.Collections;

public class MonoToSerializedDB 
{
	public static System.Type GetSerializedType (System.Type type)
	{
		if (type == typeof (DebugLog))
		{
			return typeof (DebugLogInfo);
		}
		else
		{
			Debug.LogWarning ("MonoToSerializedDB/GetSerializedType (System.Type) : " + type.ToString () + " has no SerializedVersion");
			return null;
		}
	}
}
