using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour 
{

	Player _player;

	void Start () 
	{
		_player = GetComponentInParent<Player> ();
	}


	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag == "Enemy")
			_player.TouchedByEnemy ();
	}
	
}
