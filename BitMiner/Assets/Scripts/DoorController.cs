using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour {
	public float SecondsToWaitToClose = 1;
	public bool IsLocked = true;

	Door[] _doors;
	NavMeshObstacle _nvo;
	bool _doorsAreOpen = false;
	float _remainingCooldown = 0;

	// Use this for initialization
	void Start () {
		_doors = GetComponentsInChildren<Door> ();
		_nvo = GetComponent<NavMeshObstacle> ();
	}

	// Update is called once per frame
	void Update () {
		if (_remainingCooldown > 0) {
			_remainingCooldown -= Time.deltaTime;
			if (_remainingCooldown <= 0) {
				CloseDoors ();
			}
		}
	}

	public void OpenDoors() {
		for (int i = 0; i < _doors.Length; i++) {
			_doors [i].Open ();
		}
		_nvo.enabled = false;
	}

	public void CloseDoors() {
		for (int i = 0; i < _doors.Length; i++) {
			_doors [i].Close ();
		}
		_nvo.enabled = true;
	}

	public void Unlock() {
		IsLocked = false;
	}

	void OnTriggerEnter(Collider other) {
		HandleTrigger (other);
	}

	void OnTriggerStay(Collider other) {
		HandleTrigger (other);
	}

	void HandleTrigger(Collider other) {
		if (!IsLocked && other.tag == "Player") {
			OpenDoors ();
			_remainingCooldown = SecondsToWaitToClose;
		}
	}
}
