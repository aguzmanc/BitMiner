using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour 
{
	bool _canBeHacked;
	bool _isNear;
	bool _isHacking;

	[Range(1, 20)]
	public float DisableTimeOnBackHack = 5f;
	public GameObject AButtonPrototype;
	public GameObject BButtonPrototype;
	public GameObject XButtonPrototype;
	public GameObject YButtonPrototype;

	HackButton _currentHackButton;
	GameObject [] _prototypes;


	void Awake()
	{
		_prototypes = new GameObject[4]
			{AButtonPrototype,BButtonPrototype,XButtonPrototype,YButtonPrototype};
		_isNear = false;
	}

	void Start()
	{
		_canBeHacked = true;
		_currentHackButton = _GenerateHackButton();
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Player")
			return;

		_isNear = true;
		_currentHackButton.Show();
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag != "Player")
			return;

		_isNear = false;
		_currentHackButton.Hide();
	}


	void OnTriggerStay(Collider other)
	{
		if(other.tag != "Player")
			return;

		if(_HasPressedSomething() && _canBeHacked){
			if(_HasPressedCorrectly()){
				_currentHackButton.Correct();
				Destroy(_currentHackButton.gameObject, 3);
				_currentHackButton = _GenerateHackButton();
				_currentHackButton.Show();
				_isHacking = true;
			}
			else{
				_isHacking = false;
				_currentHackButton.Incorrect();
				StartCoroutine(_WrongAnswerCoroutine());
				Destroy(_currentHackButton.gameObject, 3);
			}
		}
	}


	HackButton _GenerateHackButton()
	{
		GameObject newButton = Instantiate(_prototypes[Random.Range(0,4)], transform.position, transform.rotation);
		newButton.transform.parent = transform;

		return newButton.GetComponent<HackButton>();
	}


	IEnumerator _WrongAnswerCoroutine()
	{
		_canBeHacked = false;
		yield return new WaitForSeconds(DisableTimeOnBackHack);
		_canBeHacked = true;
		_currentHackButton = _GenerateHackButton();
		if(_isNear)
			_currentHackButton.Show();
	}


	bool _HasPressedCorrectly()
	{
		return
			(_currentHackButton.CurrentButton==HackButton.JoystickButton.A && Input.GetButtonDown("AButton")) ||
			(_currentHackButton.CurrentButton==HackButton.JoystickButton.B && Input.GetButtonDown("BButton")) ||
			(_currentHackButton.CurrentButton==HackButton.JoystickButton.X && Input.GetButtonDown("XButton")) ||
			(_currentHackButton.CurrentButton==HackButton.JoystickButton.Y && Input.GetButtonDown("YButton"));
	}

	bool _HasPressedSomething()
	{
		return 
			Input.GetButtonDown("AButton") ||
			Input.GetButtonDown("BButton") ||
			Input.GetButtonDown("XButton") ||
			Input.GetButtonDown("YButton");
	}
}
