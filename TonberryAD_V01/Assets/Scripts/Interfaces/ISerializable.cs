using UnityEngine;
using System.Collections;

interface ISerializable<T> 
{
	T GetSerializedInfo ();
}
