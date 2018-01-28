using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinTerminal : Terminal 
{
	public Player player;
	CoinTransmission _cointransmission;

	public override void Awake() {
		base.Awake();

		_cointransmission = GetComponentInChildren<CoinTransmission>();
		_cointransmission.Target = player.transform;
	}

	public override void UpdateRemainingKeys() {
		_cointransmission.DesiredProgress = ((float)NumberKeysToHack - _remainingKeysTohack)/NumberKeysToHack;
	}

	public override void Exit()	{
		_cointransmission.DesiredProgress = 0;
	}

	public override void Hack() {
		player.WinCoins ();
	}
}
