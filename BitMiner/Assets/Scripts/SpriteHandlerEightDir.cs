using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandlerEightDir : MonoBehaviour {
	private Animator animator;
	public Vector3 forward;

	Vector3[] _directions = {
		new Vector3(0, 0, -1),
		Vector3.Normalize(new Vector3(1, 0, -1)),
		new Vector3(1, 0, 0),
		Vector3.Normalize(new Vector3(1, 0, 1)),
		new Vector3(0, 0, 1),
		Vector3.Normalize(new Vector3(-1, 0, 1)),
		new Vector3(-1, 0, 0),
		Vector3.Normalize(new Vector3(-1, 0, -1))
	};

	void Start() {
		animator = this.GetComponent<Animator>();
	}

	void Update () {
		forward = transform.parent.forward;
		int animIndex = 0;
		float maxDotP = -999999;
		for (int i = 0; i < _directions.Length; i++) {
			float dotP = Vector3.Dot (forward, _directions [i]);
			if (dotP > maxDotP) {
				maxDotP = dotP;
				animIndex = i;
			}
		}
		animator.SetInteger("Direction", animIndex);
	}
}
