using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour 
{
	float _progress = 0;
	LineRenderer _line;
	ParticleSystem _particles;
	Vector3[] _linePositions;

	public Transform Target;

	[Range(0,1f)]
	public float DesiredProgress;
	

	void Awake () 
	{
		_line = GetComponentInChildren<LineRenderer>();
		_line.useWorldSpace = true;

		_particles = GetComponentInChildren<ParticleSystem>();
	}

	void Start()
	{
		_linePositions = new Vector3[4];
	}
	
	void Update () 
	{
		_progress = Mathf.Lerp(_progress, DesiredProgress, 2f * Time.deltaTime);

		// Set Line
		Vector3 p = _line.transform.position;
		Vector3 t = Target.position;

		_linePositions[0] = new Vector3(p.x, 2f, p.z);
		_linePositions[1] = new Vector3(p.x, p.y+0.5f, p.z);
		_linePositions[2] = new Vector3(t.x, p.y+0.5f, t.z);
		_linePositions[3] = new Vector3(t.x, 2f , t.z);
		_line.SetPositions(_linePositions);

		Vector3 dif = t-p;
		dif = new Vector3(dif.x, 0, dif.z);

		// Set Particle System
		_particles.transform.rotation = Quaternion.LookRotation(dif);

		_particles.startSpeed =  DesiredProgress*2 + 1f;
		_particles.emissionRate = DesiredProgress * 80;
		if(DesiredProgress == 1f) {
			_particles.emissionRate = 0;
		}

		_line.material.SetTextureOffset("_MainTex", new Vector2((1f-_progress)*2 - 1, 0f));
	}
}
