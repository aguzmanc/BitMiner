using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalChangeTexture : MonoBehaviour 
{
	public List<Texture> Textures;
	public List<Texture> EmissionTextures;


	int _idx;
	Material _mat;

	void Start () 
	{
		_idx = 0;
		_mat = GetComponent<Renderer>().material;
		StartCoroutine(_ChangeTextureCoroutine());
	}
	

	IEnumerator _ChangeTextureCoroutine()
	{
		while(true)
		{
			_idx = (_idx+1) % Textures.Count;
			_mat.SetTexture("_MainTex", Textures[_idx]);
			_mat.SetTexture("_EmissionMap", EmissionTextures[_idx]);

			yield return new WaitForSeconds(Random.Range(0.2f, 2f));

		}
	}
}
