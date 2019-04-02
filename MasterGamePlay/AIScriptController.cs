using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PhotonView))]
public class AIScriptController : MonoBehaviour 
{
	[SerializeField]
	private Renderer _Renderer;
	private void OnEnable()
	{
		if(GetComponent<PhotonView>().isMine == false)
		{
			Destroy(GetComponent<AIUnit>());
			Destroy(GetComponent<NavMeshAgent>());		
		}

		if(PhotonNetwork.isMasterClient == false)
		{
			var Mat =  _Renderer.materials;
			_Renderer.materials = new Material[1] {Mat[0]};
		}
	}	
}
