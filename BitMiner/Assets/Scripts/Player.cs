using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float MovementSpeed = 0.01f;

	float _horizontalAxis;
	float _verticalAxis;
	Rigidbody _body;
	public Vector3 direction;

	void Awake() {
		_body = GetComponent<Rigidbody>();
	}

	void Update () {
		handleMovement ();
	}

	void handleMovement() {
		_horizontalAxis = Input.GetAxis ("Horizontal");
		_verticalAxis = Input.GetAxis ("Vertical");

		//Vector3 movement = new Vector3 (0, 0, 0.01f * MovementSpeed * (Mathf.Abs(_horizontalAxis) + Mathf.Abs(_verticalAxis)));
		//Vector3 rotation = new Vector3 (0, Mathf.Atan(_verticalAxis / _horizontalAxis), 0);
		//transform.Translate(movement);
		//transform.eulerAngles = rotation;

		direction = (Quaternion.Euler (0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(_horizontalAxis, 0, _verticalAxis)) * Time.deltaTime;
		if(direction != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(direction);
			//transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
		}

		_body.transform.Translate(new Vector3(0,0,MovementSpeed * (Mathf.Abs(_horizontalAxis) + Mathf.Abs(_verticalAxis))), Space.Self);
	}
}
