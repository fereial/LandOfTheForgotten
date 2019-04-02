using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMasterUi : MonoBehaviour 
{
	private void Awake()
	{
		if(GetComponent<PhotonView>().isMine == false)
		{
			Destroy(GetComponent<MasterMinionController>());
			Destroy(GetComponent<GUISelection>());
		}
	}
}
