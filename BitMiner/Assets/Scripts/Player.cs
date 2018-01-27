using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[Range(10,100)]
	public int MovementSpeed = 40;

	float _horizontalAxis;
	float _verticalAxis;

	void Update () {
		handleMovement ();
	}

	void handleMovement() {
		_horizontalAxis = Input.GetAxis ("Horizontal");
		_verticalAxis = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (0.01f * MovementSpeed * _horizontalAxis, 0, 0.01f * MovementSpeed * _verticalAxis);
		transform.Translate(movement, Space.World);
	}
}
