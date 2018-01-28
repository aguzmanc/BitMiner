using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour {
	public float SecondsToWaitToClose = 1;
	public bool IsLocked = true;
	public EnemyController[] EnemiesLocked;
	public AudioClip OpenAudio;
	public AudioClip CloseAudio;
	public GameObject AudioSourcePrototype;

	Door[] _doors;
	NavMeshObstacle _nvo;
	bool _doorsAreOpen = false;
	float _remainingCooldown = 0;

	// Use this for initialization
	void Start () {
		_doors = GetComponentsInChildren<Door> ();
		_nvo = GetComponent<NavMeshObstacle> ();
		if (IsLocked) {
			for (int i = 0; i < EnemiesLocked.Length; i++) {
				EnemiesLocked [i].Lock ();
			}
		} else {
			_nvo.enabled = false;
		}
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
		Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (OpenAudio);
		for (int i = 0; i < _doors.Length; i++) {
			_doors [i].Open ();
		}
	}

	public void CloseDoors() {
		Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (CloseAudio);
		for (int i = 0; i < _doors.Length; i++) {
			_doors [i].Close ();
		}
	}

	public void Unlock() {
		IsLocked = false;
		_nvo.enabled = false;
		for (int i = 0; i < EnemiesLocked.Length; i++) {
			EnemiesLocked [i].Unlock ();
		}
	}

	void OnTriggerEnter(Collider other) {
		HandleTrigger (other);
	}

	void OnTriggerExit(Collider other) {
		HandleTrigger (other);
	}

	void HandleTrigger(Collider other) {
		if (!IsLocked && (other.tag == "Player" || other.tag == "Enemy")) {
			OpenDoors ();
			_remainingCooldown = SecondsToWaitToClose;
		}
	}
}
