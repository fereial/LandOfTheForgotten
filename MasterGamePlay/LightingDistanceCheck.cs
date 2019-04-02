using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingDistanceCheck : MonoBehaviour
{

	[SerializeField]
	private float _PlayerDetectionRange = 0f;
	
	[SerializeField]
	private GameObject _SpawnRef;

	
	// Update is called once per frame
	private void Update ()
	{
		PlayerToClose();
	}

	private void PlayerToClose()
	{
		foreach(var Players in PlayerNetworkManager.PlayerStats )
		{
			if((transform.position - Players.transform.position).sqrMagnitude <= _PlayerDetectionRange * _PlayerDetectionRange  )
			{
				_SpawnRef.SetActive(true);
				_SpawnRef.transform.localScale =new Vector3(_PlayerDetectionRange, 1, _PlayerDetectionRange);
				MasterObjectSpawner.AbleToBuild = false;
			}
			else
			{
				_SpawnRef.SetActive(false);
				MasterObjectSpawner.AbleToBuild = true;
			}
		}
	}
}
