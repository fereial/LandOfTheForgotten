using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScriptController : MonoBehaviour 
{
	private void OnEnable()
	{
		if(PhotonNetwork.isMasterClient == false)
		{
			Destroy(gameObject);
		}
	}
	
}
