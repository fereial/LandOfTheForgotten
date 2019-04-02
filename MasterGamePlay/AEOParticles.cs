using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AEOParticles : MonoBehaviour 
{
	[SerializeField]
	private GameObject _Particles;
	
	private IEnumerator PlayParticles()
	{
		_Particles.SetActive(true);
		yield return new WaitForSeconds(0.70f);
		_Particles.SetActive(false);
	}

	public void TrigerTheParicles()
	{
		StartCoroutine(PlayParticles());
	}


	
}