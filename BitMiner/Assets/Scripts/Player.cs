using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float MovementSpeed = 10;

	public float _horizontalAxis;
	public float _verticalAxis;

	public Transform ReferenceTransformMovement;

	void Update () {
		_horizontalAxis = Input.GetAxis ("Horizontal");
		_verticalAxis = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (0.01f * MovementSpeed * _horizontalAxis, 0, 0.01f * MovementSpeed * _verticalAxis);
		transform.Translate (ReferenceTransformMovement.TransformDirection(movement));
	}
}
