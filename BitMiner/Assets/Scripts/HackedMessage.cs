using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HackedMessage : MonoBehaviour 
{

	Animator _anim;
	public bool Hidden;

	void Awake () 
	{
		_anim = GetComponent<Animator>();	
		Hidden = true;
	}


	public void Show()
	{
		Hidden = false;
		_anim.SetTrigger("Show");
	}

	public void Hide()
	{
		Hidden = true;
		_anim.SetTrigger("Hide");
	}
}
