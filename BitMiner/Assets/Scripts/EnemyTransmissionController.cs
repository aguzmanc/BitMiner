using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransmissionController : MonoBehaviour {
	public float ShortCooldownSeconds = 3;
	public float LongCooldownSeconds = 10;

	PatrolAgent _patrolAgent;
	float _remainingCooldown = 0;

	// Use this for initialization
	void Start () {
		_patrolAgent = GetComponent<PatrolAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		_remainingCooldown -= Time.deltaTime;
		if (_remainingCooldown > 0) {
			_patrolAgent.DisableMovement ();
		} else {
			_patrolAgent.EnableMovement ();
		}
	}

	public void StartShortCooldown() {
		_remainingCooldown = Mathf.Max(_remainingCooldown, ShortCooldownSeconds);
	}

	public void StartLongCooldown() {
		_remainingCooldown = Mathf.Max(_remainingCooldown, LongCooldownSeconds);
	}
}
