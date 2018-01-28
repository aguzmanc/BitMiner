using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public int Direction = 0;
	public float OpenSpeed = 3;
	public float Width = 1;
	public float Height = 2.5f;
	public float SecondsToWaitToClose = 3;
	public bool IsLocked = true;

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
			transform.position = Vector3.Lerp(transform.position, PositionOpen, Time.deltaTime * OpenSpeed);
			if (Vector3.Distance (transform.position, PositionOpen) < 0.01f) {
				_isOpening = false;
				StartCoroutine (WaitAndClose ());
			}
		}
		if (_isClosing) {
			transform.position = Vector3.Lerp(transform.position, PositionClosed, Time.deltaTime * OpenSpeed);
			if (Vector3.Distance (transform.position, PositionClosed) < 0.01f) {
				_isClosing = false;
			}
		}
	}

	public void Open() {
		_isOpening = true;
	}

	public void Unlock() {
		IsLocked = false;
	}

	public void OnTriggerEnter(Collider other) {
		Debug.Log (other);
		if (!IsLocked && other.tag == "Player") {
			Open ();
		}
	}

	IEnumerator WaitAndClose() {
		yield return new WaitForSeconds (SecondsToWaitToClose);
		_isClosing = true;
	}
}
