using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSearcher : MonoBehaviour {

	public GameObject TransmissionPrefab;
	public Transform TransmissionOrigin;

	public List<GameObject> FoundRobots = new List<GameObject> ();
	public List<Transmission> Transmissions = new List<Transmission> ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			FoundRobots.Add (other.gameObject);
			GameObject newTransmission = Instantiate (TransmissionPrefab, TransmissionOrigin);
			Transmission t = newTransmission.GetComponent<Transmission> ();
			t.Target = other.transform;
			Transmissions.Add (t);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Enemy") {
			FoundRobots.Remove (other.gameObject);
			Transmission t = Transmissions.Find (transmission => transmission.Target == other.transform);
			Transmissions.Remove (t);
			Destroy (t.gameObject);
		}
	}
}
