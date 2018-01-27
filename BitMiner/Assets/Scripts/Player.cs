using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float MovementSpeed = 0.3f;
	public Transform Camera;

	public float _horizontalAxis;
	public float _verticalAxis;
	private Vector2 _axis;

	//enum Direction { Idle, Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight}

	void Start () {
		//transform.LookAt(Camera.position);
	}

	void Update () {
		_horizontalAxis = Input.GetAxis ("Horizontal");
		_verticalAxis = Input.GetAxis ("Vertical");

		//8-axis movement
		_axis = new Vector2(_horizontalAxis, _verticalAxis);

		if (_axis.x == -1) //Left
			transform.Translate (MovementSpeed * new Vector3 (-1, 0, 0));
		else if (_axis.x == 1) //Right
			transform.Translate (MovementSpeed * new Vector3 (1, 0, 0));
		else if (_axis.y == 1) //Up
			transform.Translate (MovementSpeed * new Vector3 (0, 0, 1));
		else if (_axis.y == -1) //Down
			transform.Translate (MovementSpeed * new Vector3 (0, 0, -1));
		else if (_axis.x > -1 && _axis.x < 0 && _axis.y > 0 && _axis.y < 1) //upLeft
			transform.Translate (MovementSpeed * new Vector3 (-1, 0, 1));
		else if (_axis.x > 0 && _axis.x < 1 && _axis.y > 0 && _axis.y < 1) //upRight
			transform.Translate (MovementSpeed * new Vector3 (1, 0, 1));
		else if (_axis.x > -1 && _axis.x < 0 && _axis.y > -1 && _axis.y < 0) //downLeft
			transform.Translate (MovementSpeed * new Vector3 (-1, 0, -1));
		else if (_axis.x > 0 && _axis.x < 1 && _axis.y > -1 && _axis.y < 0) //downRight
			transform.Translate (MovementSpeed * new Vector3 (1, 0, -1));
		else
			transform.Translate (0, 0, 0);
	}
}
