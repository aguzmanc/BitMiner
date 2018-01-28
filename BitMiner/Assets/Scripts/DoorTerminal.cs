using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTerminal : Terminal 
{
	public Door Door;
	Transmission _transmission;

	public override void Awake()
	{
		base.Awake();

		_transmission = GetComponentInChildren<Transmission>();
	}


	public override void UpdateRemainingKeys()
	{
		_transmission.DesiredProgress =  ((float)NumberKeysToHack - _remainingKeysTohack)/NumberKeysToHack;
	}

	public override void Exit()
	{
		_transmission.DesiredProgress = 0;
	}
}
