using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[Range(5, 50)]
	public int MovementSpeed = 20;
	public ParticleSystem DeathEffect;
	public ParticleSystem InnerDeathEffect;
	public GameObject Sprite;
	public Text ScoreUI;

	float _horizontalAxis;
	float _verticalAxis;
	Rigidbody _body;
	Vector3 _direction;
	bool _canMove = true;
	int _score = 0;

	void Awake() {
		_body = GetComponent<Rigidbody>();
		ScoreUI.text = "000";
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("Enemy")) {
			Debug.Log ("KILLED");
			if (_canMove) {
				DeathEffect.Play ();
				InnerDeathEffect.Play ();
				_canMove = false;
			}
			Sprite.SetActive (false);
			StartCoroutine (Respawn());
		}
	}

	void Update () {
		if (Sprite.activeInHierarchy)
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

		_body.transform.Translate(new Vector3(0,0, 0.01f * MovementSpeed * (Mathf.Abs(_horizontalAxis) + Mathf.Abs(_verticalAxis))), Space.Self);
	}

	public void WinCoins(int value) {
		_score += value;

		string prefix = "";
		if (_score < 10)
			prefix = "00";
		else if (_score >= 10 && _score < 99)
			prefix = "0";
		else
			prefix = "";

		ScoreUI.text = prefix + _score.ToString();
	}

	IEnumerator Respawn() {
		yield return new WaitForSeconds (1);
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene (0);
	}
}
