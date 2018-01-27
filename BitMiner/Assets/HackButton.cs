using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HackButton : MonoBehaviour 
{
	public enum JoystickButton {A,B,X,Y};
	public JoystickButton CurrentButton;

	Animator _anim;

	void Awake () 
	{
		_anim = GetComponent<Animator>();	
	}


	public void Show()
	{
		_anim.SetTrigger("Show");
	}

	public void Hide()
	{
		_anim.SetTrigger("Hide");
	}

	public void Correct()
	{
		_anim.SetTrigger("Correct");
	}


	public void Incorrect()
	{
		_anim.SetTrigger("Incorrect");
	}
}
