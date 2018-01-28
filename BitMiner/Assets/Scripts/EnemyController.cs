using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float ShortCooldownSeconds = 3;
	public float LongCooldownSeconds = 10;
	bool _isLocked = false;
	bool _wasLocked = false;

	PatrolAgent _patrolAgent;
	float _remainingCooldown = 0;

	// Use this for initialization
	void Start () {
		_patrolAgent = GetComponent<PatrolAgent> ();
		_isLocked = !_patrolAgent.StartOnAwake;
		_wasLocked = _isLocked;
	}
	
	// Update is called once per frame
	void Update () {
		if (_isLocked) {
			_patrolAgent.DisableMovement ();
		} else if (_wasLocked && !_isLocked) {
			_patrolAgent.StartPatrolling ();
			_wasLocked = false;
		} else if (!_isLocked) {
			if (_remainingCooldown > 0) {
				_remainingCooldown -= Time.deltaTime;
				_patrolAgent.DisableMovement ();
			} else {
				_patrolAgent.EnableMovement ();
			}
		}
	}

	public void StartShortCooldown() {
		_remainingCooldown = Mathf.Max(_remainingCooldown, ShortCooldownSeconds);
	}

	public void StartLongCooldown() {
		_remainingCooldown = Mathf.Max(_remainingCooldown, LongCooldownSeconds);
	}

	public void Unlock() {
		_isLocked = false;
	}

	public void Lock() {
		_isLocked = true;
	}
}
