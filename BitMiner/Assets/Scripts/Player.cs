using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float MovementSpeed = 0.2f;
	public ParticleSystem deathEffect;
	public ParticleSystem innerDeathEffect;
	public GameObject sprite;

	float _horizontalAxis;
	float _verticalAxis;
	Rigidbody _body;
	Vector3 _direction;
	bool _canPlay = true;

	void Awake() {
		_body = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Enemy")) {
			Debug.Log ("KILLED");
			if (_canPlay) {
				deathEffect.Play ();
				innerDeathEffect.Play ();
				_canPlay = false;
			}
			sprite.SetActive (false);
		}
	}

	void Update () {
		if (sprite.activeInHierarchy)
			handleMovement ();
	}

	void handleMovement() {
		_horizontalAxis = Input.GetAxis ("Horizontal");
		_verticalAxis = Input.GetAxis ("Vertical");

		//Vector3 movement = new Vector3 (0, 0, 0.01f * MovementSpeed * (Mathf.Abs(_horizontalAxis) + Mathf.Abs(_verticalAxis)));
		//Vector3 rotation = new Vector3 (0, Mathf.Atan(_verticalAxis / _horizontalAxis), 0);
		//transform.Translate(movement);
		//transform.eulerAngles = rotation;

		_direction = (Quaternion.Euler (0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(_horizontalAxis, 0, _verticalAxis)) * Time.deltaTime;
		if(_direction != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(_direction);
			//transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
		}

		_body.transform.Translate(new Vector3(0,0,MovementSpeed * (Mathf.Abs(_horizontalAxis) + Mathf.Abs(_verticalAxis))), Space.Self);
	}
}
