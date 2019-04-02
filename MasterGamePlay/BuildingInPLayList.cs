using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInPLayList : MonoBehaviour 
{
	private void OnEnable()
	{
		if( PhotonNetwork.isMasterClient)
		{
			MasterObjectSpawner.BuildingInPlay.Add(this);
		}
	}

	private void OnDisable()
	{
		if( PhotonNetwork.isMasterClient)
		{
			MasterObjectSpawner.BuildingInPlay.Remove(this);;
		}
	}
}
