using System;
using System.IO;
using UnityEngine;


public class MinionRangeAttack : MonoBehaviour 
{
	[SerializeField]
	private  GameObject _SpawnREf;
    [SerializeField]
    private  GameObject _ORb;
    private MinionType _Type;
    private void OnEnable()
    {
        _Type = GetComponent<AIUnit>()._MinionType;
    }

    internal void RangeAttack(Vector3 AttackDesticnation )
    {
        GameObject obj =  PhotonNetwork.Instantiate(Path.Combine("Prefabs/Test","VFX_Ghost_Orb"),_SpawnREf.transform.position, _SpawnREf.transform.rotation, 0);
        GhostBehaviorAttack OrbAttack = obj.GetComponent<GhostBehaviorAttack>();
        OrbAttack.Type = _Type;
        AttackDesticnation.y = _SpawnREf.transform.position.y;
        OrbAttack.AttackPostion = AttackDesticnation;
    }
}
