using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour {

	void Update () {
		float _horizontalAxis = Input.GetAxis ("Horizontal");
		float _verticalAxis = Input.GetAxis ("Vertical");
		Vector2 _axis = new Vector2(_horizontalAxis, _verticalAxis);

		if (_axis.x == -1) //Left
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.x == 1) //Right
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.y == 1) //Up
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.y == -1) //Down
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.x > -1 && _axis.x < 0 && _axis.y > 0 && _axis.y < 1) //upLeft
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.x > 0 && _axis.x < 1 && _axis.y > 0 && _axis.y < 1) //upRight
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.x > -1 && _axis.x < 0 && _axis.y > -1 && _axis.y < 0) //downLeft
			_axis = _axis; // TODO Replace here with respective sprite
		else if (_axis.x > 0 && _axis.x < 1 && _axis.y > -1 && _axis.y < 0) //downRight
			_axis = _axis; // TODO Replace here with respective sprite
		else
			_axis = _axis; // TODO Replace here with respective sprite
	}
}
