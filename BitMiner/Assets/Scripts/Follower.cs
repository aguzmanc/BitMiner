using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

	public Transform Target;
	public float Smoothness = 1f;


	void Start()
	{
	}

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, Target.position, Smoothness * Time.deltaTime);
	}
}
