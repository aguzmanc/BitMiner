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
		handleButtons ();
	}

	void handleMovement() {
		_horizontalAxis = Input.GetAxis ("Horizontal");
		_verticalAxis = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (0.01f * MovementSpeed * _horizontalAxis, 0, 0.01f * MovementSpeed * _verticalAxis);
		transform.Translate(movement, Space.World);
	}

	void handleButtons () {
		if (Input.GetButtonDown ("Cross")) { //X Button on PS4 - A Button on XBox
			Debug.Log ("X / A pressed");
		} else if (Input.GetButtonDown ("Square")) { //□ Button on PS4 - X Button on XBox
			Debug.Log ("□ / X pressed");
		} else if (Input.GetButtonDown ("Circle")) { //○ Button on PS4 - B Button on XBox
			Debug.Log ("○ / B pressed");
		} else if (Input.GetButtonDown ("Triangle")) { //△ Button on PS4 - Y Button on XBox
			Debug.Log ("△ / Y pressed");
		}
	}
}
