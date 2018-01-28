using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinTerminal : Terminal 
{
	public Player player;
	[Range(1,5)]
	public int Budget = 1;
	CoinTransmission _coinTransmission;
	ParticleSystem _bitcoinParticleSystem;

	public override void Awake() {
		base.Awake();

		_coinTransmission = GetComponentInChildren<CoinTransmission>();
		_coinTransmission.Target = player.transform;

		_bitcoinParticleSystem = GetComponentInChildren<ParticleSystem>();
	}

	public override void UpdateRemainingKeys() {
		_coinTransmission.DesiredProgress = ((float)NumberKeysToHack - _remainingKeysTohack)/NumberKeysToHack;
	}

	public override void Exit()	{
		_coinTransmission.DesiredProgress = 0;
	}

	public override void Hack() {
		_bitcoinParticleSystem.Play ();
		player.WinCoins (Budget);
	}
}
