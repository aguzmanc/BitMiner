using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour 
{
	Rigidbody _body;

	public float Velocity = 2f;
	public Transform ReferenceTransformMovement;


	void Awake()
	{
		_body = GetComponent<Rigidbody>();	
	}

	void FixedUpdate () 
	{
		float hh = Input.GetAxis("Horizontal");
		float vv = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(hh, 0, vv).normalized;
		direction = ReferenceTransformMovement.TransformDirection(direction).normalized;
		if(direction != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(direction);

		_body.transform.Translate(
			new Vector3(0,0,direction.magnitude * Velocity * Time.deltaTime), Space.Self);
	}
}
