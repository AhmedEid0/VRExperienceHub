using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	// Start is called before the first frame update
	void Update()
	{
		foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(keyCode))
			{
				Debug.Log("Key pressed: " + keyCode);
			}
		}
	}
}
