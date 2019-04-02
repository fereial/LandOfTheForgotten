using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterLightingDmg : MonoBehaviour 
{
	[SerializeField]
	private float _LightingRadius = 10f;
	[SerializeField]
	private LayerMask _DamageLayer;
    [SerializeField]
    private float _DamageTimer = 3f;
	[SerializeField]
	private float _LightingDmg = -30f;
	[SerializeField]
	private float _ClayCost = 50f;
	[SerializeField]
	private float _InitialCost = 50f;
	[SerializeField]
	private Material _MatRef;
	[SerializeField]
	private GameObject _ParticlesPrefab;
	private MeshRenderer _Mesh;

	private ParticleSystem _Lighting;


	private void OnEnable()
	{
		_Lighting = _ParticlesPrefab.GetComponent<ParticleSystem>();
		// GetComponent<PhotonView>().RPC("SendRPC_LightningTimer", PhotonTargets.All);
		
        AkSoundEngine.PostEvent("Play_Lightning_BuildUp", gameObject);

		ResourcePoolEvents.OnDepletePoolEvent(_ClayCost);
		if(PhotonNetwork.isMasterClient == true)
		{
			GetComponent<PhotonView>().RPC("SendRPC_LightningTimer", PhotonTargets.All);
			StartCoroutine(ShootLighting());
		
		}
		_Mesh = GetComponent<MeshRenderer>();	
	}



    private void OnDisable()
    {
     
		_Lighting.gameObject.SetActive(false);
    }
	
    private IEnumerator ShootLighting()
	{
		yield return  new WaitForSeconds(_DamageTimer);
		Collider[] PlayerInArea = Physics.OverlapSphere(transform.position, _LightingRadius , _DamageLayer);
        
        foreach (var player in PlayerInArea  )
		{

			PhotonView PlayerPhotonView = player.gameObject.GetComponent<PhotonView>();
			
			if (PlayerPhotonView != null)
        	{
				SendRPC_Damage(PlayerPhotonView);
			}
		}
		
		if(GetComponent<PhotonView>().isMine)
		{
            PhotonNetwork.Destroy(gameObject);
		}
	}
	
	private IEnumerator ChangeMesh()
	{
		yield return  new WaitForSeconds(_DamageTimer - 1f);
		AkSoundEngine.PostEvent("Play_Lighting_Strike", gameObject);
		_Lighting.gameObject.SetActive(true);
        _Mesh.material = _MatRef;
	}

	private void SendRPC_Damage(PhotonView PlayerPhotonView)
    {   
     	PlayerPhotonView.RPC("RPC_DamagePlayer", PlayerPhotonView.owner, _LightingDmg, false );  
		GetComponent<PhotonView>().RPC("RPC_SetMinionType", PhotonTargets.All, MinionType.Mummy); 
    }

	
	public float GetCost()
	{
		return _ClayCost;
	}

	[PunRPC]
	public void SendRPC_LightningTimer()
	{
		StartCoroutine(ChangeMesh());
	}
}
