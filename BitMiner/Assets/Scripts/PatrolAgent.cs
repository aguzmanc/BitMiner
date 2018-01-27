using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour {
	public Transform[] Points;
	public float Speed = 5f;
	[Range(1, 99)]
	public int MinTimeToChangeDirection = 20;
	[Range(2, 100)]
	public int MaxTimeToChangeDirection = 40;
	public GameObject PointsContainer;

	int _destPointIndex;
	NavMeshAgent _agent;
	bool _isPatrolling = true;
	int _direction = 1; // -1 or 1
	public bool _isStopped = false;

	// Use this for initialization
	void Start () {
		InitializePoints ();
		RandomizeStartingPosition ();
		_agent = GetComponent<NavMeshAgent> ();
		_agent.autoBraking = false;
		_agent.speed = Speed;
		GoToNextPoint ();
		StartCoroutine (ChangeDirectionAfterRandomTime ());
	}

	void InitializePoints() {
		if (PointsContainer) {
			Transform[] obtainedPoints = PointsContainer.GetComponentsInChildren<Transform> ();
			Points = new Transform[obtainedPoints.Length - 1];
			for (int i = 1; i < obtainedPoints.Length; i++) {
				Points [i - 1] = obtainedPoints [i];
			}
		}
	}

	void RandomizeStartingPosition() {
		if (Random.value < 0.5f) {
			_direction = -1;
		} else {
			_direction = 1;
		}
		int randomPositionIndex = Random.Range (0, Points.Length);
		_destPointIndex = randomPositionIndex;
		transform.position = Points [_destPointIndex].position;

	}
	
	// Update is called once per frame
	void Update () {
		_agent.speed = Speed;
		if (_isStopped) {
			_agent.isStopped = true;
		} else {
			_agent.isStopped = false;
			if (_isPatrolling) {
				if (!_agent.pathPending && _agent.remainingDistance < 0.5f) {
					GoToNextPoint ();
				}
			}
		}
	}

	void GoToNextPoint() {
		int newPointIndex = _destPointIndex + _direction;
		_destPointIndex = (newPointIndex % Points.Length + Points.Length) % Points.Length;
		_agent.destination = Points[_destPointIndex].position;
	}

	public void GoToSpecificPoint(Vector3 point) {
		_agent.destination = point;
		_isPatrolling = false;
	}

	public void KeepPatrolling() {
		_agent.destination = Points[_destPointIndex].position;
		_isPatrolling = true;
	}

	public void StopForSeconds(float secondsToStop) {
		StartCoroutine (StopAndWait (secondsToStop));
	}

	IEnumerator StopAndWait(float secondsToStop) {
		_isStopped = true;
		yield return new WaitForSeconds (secondsToStop);
		_isStopped = false;
	}

	IEnumerator ChangeDirectionAfterRandomTime() {
		while (true) {
			yield return new WaitForSeconds (Random.Range(MinTimeToChangeDirection, MaxTimeToChangeDirection));
			_direction *= -1;
			GoToNextPoint ();
		}
	}
}
