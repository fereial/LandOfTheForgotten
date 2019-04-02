using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScripController : MonoBehaviour 
{
	private BuildinSpawnerController _BuildSpawner;

	private void Awake()
	{
		_BuildSpawner = GetComponent<BuildinSpawnerController>();
		if(PhotonNetwork.isMasterClient)
		{
			Destroy(_BuildSpawner);
		}
	}


}
