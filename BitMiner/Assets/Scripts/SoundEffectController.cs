using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour {
	AudioSource _audioSource;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(AudioClip clip) {
		if (!_audioSource) {
			_audioSource = GetComponent<AudioSource> ();
		}
		_audioSource.playOnAwake = true;
		_audioSource.loop = false;
		_audioSource.clip = clip;
		_audioSource.Play ();
		Destroy (gameObject, 10);
	}
}
