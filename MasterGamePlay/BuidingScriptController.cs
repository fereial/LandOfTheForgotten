using UnityEngine;

public class BuidingScriptController : MonoBehaviour 
{
	private BuildinSpawnerController _SpawnerRer;

	private void OnEnable()
	{
		_SpawnerRer = GetComponent<BuildinSpawnerController>();
		if(PhotonNetwork.isMasterClient ==false)
		{
			Destroy(_SpawnerRer);
		}
	}

}
