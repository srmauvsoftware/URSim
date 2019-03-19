using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	public static bool collision;

	void Start() {
		collision = false;
	}

	void OnTriggerEnter(Collider other)
    {
		collision = true;
		Debug.Log("entered");
    }
}
