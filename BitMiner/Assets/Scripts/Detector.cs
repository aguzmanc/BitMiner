using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detector : MonoBehaviour {
	public float Range = 5f;
	[Range(0, 90)]
	public float DegreesOfVision = 30;
	[Range(0.1f, 2)]
	public float Accuracy = 1;
	public string TagToDetect = "Player";
	public AudioClip DetectedPlayerAudio;
	public GameObject AudioSourcePrototype;

	PatrolAgent _patrolAgent;
	bool _hasDetected;

	// Use this for initialization
	void Start () {
		_patrolAgent = GetComponent<PatrolAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool foundTarget = false;
		Vector3 targetPosition = Vector3.forward;
		for (float i = -DegreesOfVision; i < DegreesOfVision; i += Accuracy) {
			Ray ray = new Ray (transform.position, Quaternion.Euler (0, i, 0) * transform.forward);
			RaycastHit hit;
			Color rayColor = Color.blue;
			if (Physics.Raycast (ray, out hit, Range)) {
				if (hit.collider.tag == TagToDetect) {
					foundTarget = true;
					targetPosition = hit.collider.transform.position;
					rayColor = Color.green;
				}
			}
			Debug.DrawRay (ray.origin, ray.direction * Range, rayColor);
		}
		if (foundTarget && _patrolAgent) {
			if (!_hasDetected) {
				Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (DetectedPlayerAudio);
				_hasDetected = true;
			}
			_patrolAgent.GoToSpecificPoint (targetPosition);
		} else {
			_patrolAgent.KeepPatrolling ();
			_hasDetected = false;
		}
	}
}
