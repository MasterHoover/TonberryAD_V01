using UnityEngine;
using System.Collections;

public class TestCombat : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
		{
			CombatManager.Instance.StartCombat ();
		}
		else if (Input.GetKeyDown (KeyCode.Alpha0))
		{
			CombatManager.Instance.EndCombat ();
		}
	}
}
