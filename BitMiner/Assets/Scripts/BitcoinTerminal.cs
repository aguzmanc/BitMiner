using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinTerminal : Terminal 
{
	public AudioClip BitcoinAudio;
	Player player;
	[Range(1,5)]
	public int Budget = 1;

	CoinTransmission _coinTransmission;
	ParticleSystem _bitcoinParticleSystem;

	public override void Awake() {
		base.Awake();

		_bitcoinParticleSystem = GetComponentInChildren<ParticleSystem>();
		_coinTransmission = GetComponentInChildren<CoinTransmission>();
	}


	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		_coinTransmission.Target = player.transform;
	}


	public override void UpdateRemainingKeys() {
		_coinTransmission.DesiredProgress = ((float)NumberKeysToHack - _remainingKeysTohack)/NumberKeysToHack;
	}

	public override void Exit()	{
		_coinTransmission.DesiredProgress = 0;
	}

	public override void Hack() {
		Instantiate (AudioSourcePrototype).GetComponent<SoundEffectController> ().Play (BitcoinAudio);
		_bitcoinParticleSystem.Play ();
		player.WinCoins (Budget);
	}
}
