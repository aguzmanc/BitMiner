using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour 
{
	public float Velocity = 2f;
	public Transform ReferenceTransformMovement;

	public float hh;
	void Update () 
	{
		hh = Input.GetAxis("Horizontal");
		float vv = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(hh, 0, vv).normalized;
		transform.TransformDirection(*)
		//direction = ReferenceTransformMovement.TransformDirection(direction).normalized;
		transform.rotation = Quaternion.LookRotation(direction);

		transform.Translate(
			new Vector3(0,0,direction.magnitude * Velocity * Time.deltaTime), Space.Self);
	}
}
