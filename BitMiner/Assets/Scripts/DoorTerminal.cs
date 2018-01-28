using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTerminal : Terminal 
{
	public DoorController Doors;
	Transmission _transmission;

	public override void Awake()
	{
		base.Awake();

		_transmission = GetComponentInChildren<Transmission>();
		_transmission.Target = Doors.transform;
	}


	public override void UpdateRemainingKeys()
	{
		_transmission.DesiredProgress =  ((float)NumberKeysToHack - _remainingKeysTohack)/NumberKeysToHack;
	}

	public override void Exit()
	{
		_transmission.DesiredProgress = 0;
	}


	public override void Hack() 
	{
		Doors.Unlock();
	}
}
