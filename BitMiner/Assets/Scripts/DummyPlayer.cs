using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour 
{
	public float Velocity = 2f;

	public float hh;
	void Update () 
	{
		hh = Input.GetAxis("Horizontal");
		float vv = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(hh, 0, vv).normalized;
		transform.rotation = Quaternion.LookRotation(direction);
	}
}
