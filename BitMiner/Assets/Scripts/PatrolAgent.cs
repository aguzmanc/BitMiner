using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour {
	public Transform[] Points;
	public float Speed = 5f;
	public GameObject PointsContainer;
	int _destPointIndex;
	NavMeshAgent _agent;
	bool _isPatrolling;

	// Use this for initialization
	void Start () {
		if (PointsContainer) {
			Transform[] obtainedPoints = PointsContainer.GetComponentsInChildren<Transform> ();
			Points = new Transform[obtainedPoints.Length - 1];
			for (int i = 1; i < obtainedPoints.Length; i++) {
				Points [i - 1] = obtainedPoints [i];
			}
		}
		transform.position = Points [0].position;
		_agent = GetComponent<NavMeshAgent> ();
		_agent.autoBraking = false;
		_agent.speed = Speed;
		GoToNextPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		_agent.speed = Speed;
		if (_isPatrolling) {
			if (!_agent.pathPending && _agent.remainingDistance < 0.5f) {
				GoToNextPoint ();
			}
		}
	}

	void GoToNextPoint() {
		_destPointIndex = (_destPointIndex + 1) % Points.Length;
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
}
