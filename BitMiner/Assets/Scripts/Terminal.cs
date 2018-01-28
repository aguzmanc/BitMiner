using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour 
{
	protected bool _canBeHacked;
	protected bool _isNear;
	protected bool _isHacking;
	protected bool _isHacked;
	protected int _remainingKeysTohack;
	protected HackButton _currentHackButton;
	protected GameObject [] _prototypes;

	[Range(5, 50)]
	public int NumberKeysToHack = 10;

	public float SecondsBeforeRetract = 1f;

	[Range(1, 20)]
	public float DisableTimeOnBackHack = 5f;
	public bool CanBeHackedMultipleTimes = false;
	public GameObject AButtonPrototype;
	public GameObject BButtonPrototype;
	public GameObject XButtonPrototype;
	public GameObject YButtonPrototype;
	public GameObject HackedMessagePrototype;
	public AudioClip HackingAudio;
	public AudioClip SuccessAudio;
	public AudioClip FailAudio;
	public GameObject AudioSourcePrototype;

	HackedMessage _hackedMessage;


	public virtual void Awake()
	{
		_prototypes = new GameObject[4]
			{AButtonPrototype,BButtonPrototype,XButtonPrototype,YButtonPrototype};
		_isNear = false;
		_isHacked = false;

		_hackedMessage = Instantiate (HackedMessagePrototype, transform.position, transform.rotation).GetComponent<HackedMessage>();
	}

	void Start()
	{
		_canBeHacked = true;
		_currentHackButton = _GenerateHackButton();
		_remainingKeysTohack = NumberKeysToHack;
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Player")
			return;

		_isNear = true;
		if (_canBeHacked) {
			_currentHackButton.Show ();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag != "Player")
			return;
	
		_isNear = false;
		if(_currentHackButton != null  && ! _currentHackButton.Hidden)
			_currentHackButton.Hide();
		_remainingKeysTohack = NumberKeysToHack;
		
		if(_retractCoroutine != null)
			StopCoroutine(_retractCoroutine);
		
		if (!_isHacked) {
			Exit ();
		}

		if (_isHacked && CanBeHackedMultipleTimes) {
			_isHacked = false;
			_canBeHacked = true;
		}
	}


	void OnTriggerStay(Collider other)
	{
		if(other.tag != "Player")
			return;
	
		if (_canBeHacked && _currentHackButton.Hidden) {
			_currentHackButton.Show ();
		}

		if(_HasPressedSomething() && _canBeHacked){
			//Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (HackingAudio);
			if(HasPressedCorrectly()){
				_remainingKeysTohack--;
				_currentHackButton.Correct();
				Destroy(_currentHackButton.gameObject, 3);
				_currentHackButton = _GenerateHackButton ();
				if (_remainingKeysTohack > 0) {
					_currentHackButton.Show ();
				}

				if(!_isHacking) {
					_isHacking = false;
				}

				if(_retractCoroutine != null)
					StopCoroutine(_retractCoroutine);

				_retractCoroutine = StartCoroutine(_RetractCoroutine());
			}
			else{
				Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (FailAudio);
				_isHacking = false;
				_currentHackButton.Incorrect();
				StartCoroutine(_WrongAnswerCoroutine(_currentHackButton));

				_remainingKeysTohack = NumberKeysToHack;
			}

			UpdateRemainingKeys();

			if(_remainingKeysTohack==0) {
				_isHacked = true;
				Hack();
				_hackedMessage.Show ();
				Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (SuccessAudio);

				StopCoroutine(_retractCoroutine);
				if (!CanBeHackedMultipleTimes) {
					//_currentHackButton.Hide ();
					Destroy (this);
				} else {
					_canBeHacked = false;
					StartCoroutine (HideMessageAfterSeconds ());
				}
			}
		}

	}


	public  virtual void UpdateRemainingKeys() {}
	public virtual void Exit() {}
	public virtual void Hack() {}




	HackButton _GenerateHackButton()
	{
		GameObject newButton = Instantiate(_prototypes[Random.Range(0,4)], transform.position, transform.rotation);
		newButton.transform.parent = transform;

		return newButton.GetComponent<HackButton>();
	}


	IEnumerator _WrongAnswerCoroutine(HackButton oldButton)
	{
		_canBeHacked = false;
		_currentHackButton = _GenerateHackButton();
		yield return new WaitForSeconds(DisableTimeOnBackHack);
		Destroy(oldButton.gameObject);
		_canBeHacked = true;
		
		if(_isNear)
			_currentHackButton.Show();
	}


	public virtual bool HasPressedCorrectly()
	{
		// Debug.Log(_currentHackButton.CurrentButton);
		Debug.Log(name);

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

	IEnumerator HideMessageAfterSeconds() {
		yield return new WaitForSeconds (3);
		_hackedMessage.Hide ();
	}

	Coroutine _retractCoroutine;
	IEnumerator _RetractCoroutine()
	{
		yield return new WaitForSeconds(SecondsBeforeRetract);

		while(true) {
			_remainingKeysTohack = Mathf.Min(_remainingKeysTohack+1, NumberKeysToHack);
			UpdateRemainingKeys();
			yield return new WaitForSeconds(0.5f);
		}
	}
}
