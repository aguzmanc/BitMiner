using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	
	void Update () {
		transform.eulerAngles = Camera.main.transform.eulerAngles;
	}
}
