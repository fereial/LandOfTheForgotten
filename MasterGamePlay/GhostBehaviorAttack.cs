// ======================================================================
//    Author    	 : Eial Ferencz Appel 
//    Version        : 1.0
//    Program        : Unity 2018.2.18f1
// 	  (C) Copyright 2019 All rights reserved.
// ======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GhostBehaviorAttack : MonoBehaviour
{
    [SerializeField]
    private float _OrbVelocity = 10f;
    [SerializeField]
    private LayerMask _Layer;
    [SerializeField]
    private float _Damage = 10f;
    internal Vector3 AttackPostion;
    internal MinionType Type;
    private Rigidbody _RB;
    private PhotonView _PhotonView;
    [SerializeField]
    private GameObject _Particles;
    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        _PhotonView = GetComponent<PhotonView>();
        StartCoroutine(LifeSpan());
    }

    private void Start()
    {
        Debug.DrawLine(transform.position, AttackPostion, Color.red, 10f );
        
    }

    private void FixedUpdate()
    {
        if (_PhotonView.isMine)
        {
            _RB.AddForce(transform.forward * _OrbVelocity);
        }
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSecondsRealtime(3);
        if (_PhotonView.isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_PhotonView.isMine)
        {
            StartCoroutine(SpawnParticles());
            PhotonView PlayerPhotonView = other.gameObject.GetComponent<PhotonView>();
            AkSoundEngine.PostEvent("Play_Ghost_Attacking_Enemy", gameObject);
            SendRPC_Damage(PlayerPhotonView);
            if (_PhotonView.isMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }

        }
    }

    public void SendRPC_Damage(PhotonView PlayerPhotonView)
    {
        if (PlayerPhotonView != null)
        {
            PlayerPhotonView.RPC("RPC_DamagePlayer", PlayerPhotonView.owner, _Damage, false);
        }
    }

    private IEnumerator SpawnParticles()
    {
        _Particles.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _Particles.SetActive(false);
    }

}
