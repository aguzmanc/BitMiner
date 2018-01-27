using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

	public Transform Player;
	public float Smoothness = 0.125f;
	public Vector3 PlayerOffset;

	void FixedUpdate () {
		Vector3 desiredPosition = Player.position + PlayerOffset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, Smoothness);
		transform.position = smoothedPosition;

		transform.LookAt (Player);
	}
}
