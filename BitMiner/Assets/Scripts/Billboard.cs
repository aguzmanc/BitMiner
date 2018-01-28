using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	
	void Update () {
		Vector3 direction = Camera.main.transform.eulerAngles - transform.eulerAngles;
		if (Vector3.Magnitude (direction) > 1) {
			transform.Rotate (direction, Space.World);
		}
	}
}
