using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public int Direction = 0;
	public float OpenSpeed = 3;
	public float Width = 1;
	public float Height = 2.5f;

	bool _isOpening = false;
	bool _isClosing = false;
	Vector3 PositionClosed;
	Vector3 PositionOpen;

	// Use this for initialization
	void Start () {
		PositionClosed = transform.position;
		switch (Direction) {
		case 0:
			PositionOpen = transform.position + new Vector3 (0, 0, Width);
			break;
		case 1:
		default:
			PositionOpen = transform.position + new Vector3 (0, 0, -Width);
			break;

		}
	}

	// Update is called once per frame
	void Update () {
		if (_isOpening) {
			_isClosing = false;
			transform.position = Vector3.Lerp(transform.position, PositionOpen, Time.deltaTime * OpenSpeed);
			if (Vector3.Distance (transform.position, PositionOpen) < 0.001f) {
				_isOpening = false;
			}
		}
		if (_isClosing) {
			_isOpening = false;
			transform.position = Vector3.Lerp(transform.position, PositionClosed, Time.deltaTime * OpenSpeed);
			if (Vector3.Distance (transform.position, PositionClosed) < 0.001f) {
				_isClosing = false;
			}
		}
	}

	public void Open() {
		_isOpening = true;
		_isClosing = false;
	}

	public void Close() {
		_isClosing = true;
		_isOpening = false;
	}
}
