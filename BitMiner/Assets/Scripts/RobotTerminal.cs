using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTerminal : Terminal  {

	RobotSearcher _robotSearcher;

	public override void Awake() {
		base.Awake();

		_robotSearcher = GetComponentInChildren<RobotSearcher> ();
		//_transmission = GetComponentInChildren<Transmission>();
		//_transmission.Target = Doors.transform;
	}


	public override void UpdateRemainingKeys() {
		for (int i = 0; i < _robotSearcher.Transmissions.Count; i++) {
			_robotSearcher.Transmissions[i].DesiredProgress =  ((float)NumberKeysToHack - _remainingKeysTohack)/NumberKeysToHack;
		}
	}

	public override void Exit() {
		for (int i = 0; i < _robotSearcher.Transmissions.Count; i++) {
			_robotSearcher.Transmissions [i].DesiredProgress = 0;
		}
	}


	public override void Hack() {
		for (int i = 0; i < _robotSearcher.FoundRobots.Count; i++) {
			_robotSearcher.FoundRobots [i].GetComponent<EnemyController> ().StartLongCooldown ();
		}
	}

	public override bool HasPressedCorrectly() {
		bool isCorrect = base.HasPressedCorrectly ();
		if (isCorrect) {
			for (int i = 0; i < _robotSearcher.FoundRobots.Count; i++) {
				_robotSearcher.FoundRobots [i].GetComponent<EnemyController> ().StartShortCooldown ();
			}
		}
		return isCorrect;
	}
}
