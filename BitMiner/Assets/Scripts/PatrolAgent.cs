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
	public bool StartOnAwake = true;

	int _destPointIndex;
	bool _isStopped = false;
	NavMeshAgent _agent;
	bool _isPatrolling = true;
	int _direction = 1; // -1 or 1

	// Use this for initialization
	void Start () {
		InitializePoints ();
		_agent = GetComponent<NavMeshAgent> ();
		_agent.autoBraking = false;
		_agent.speed = Speed;
		_isStopped = true;
		if (StartOnAwake) {
			StartPatrolling ();
		}

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

	/*void RandomizeStartingPosition() {
		if (Random.value < 0.5f) {
			_direction = -1;
		} else {
			_direction = 1;
		}
		int randomPositionIndex = Random.Range (0, Points.Length);
		_destPointIndex = randomPositionIndex;
		transform.position = Points [_destPointIndex].position;
	}*/

	public void StartPatrolling() {
		_agent.destination = Points[0].position;
		StartCoroutine (ChangeDirectionAfterRandomTime ());
		_isStopped = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_isStopped) {
			_agent.isStopped = true;
		} else {
			_agent.isStopped = false;
			_agent.speed = Speed;
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

	public void EnableMovement() {
		_isStopped = false;
	}

	public void DisableMovement() {
		_isStopped = true;
	}

	IEnumerator ChangeDirectionAfterRandomTime() {
		while (true) {
			yield return new WaitForSeconds (Random.Range(MinTimeToChangeDirection, MaxTimeToChangeDirection));
			_direction *= -1;
			GoToNextPoint ();
		}
	}
}
