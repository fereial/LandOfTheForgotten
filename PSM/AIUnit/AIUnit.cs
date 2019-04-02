using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIUnit : MonoBehaviour
{
    #region GlobalVariables
    public static List<AIUnit> AIInPLayList = new List<AIUnit>();

    [SerializeField]
    private AI_State _CurrentSate;
    [SerializeField, Range(0f, 100f)]
    internal float EnemyDetectionRadius;
    [SerializeField]
    internal float AgentStopDistance = 0;
    [SerializeField]
    internal float Damage = -10;
    [SerializeField]
    internal float AOEDamage = -50;
    [SerializeField]
    internal float AttackCD = 0;
    [SerializeField, Range(0f, 50f)]
    internal float AttackRange = 0f;
    [SerializeField]
    private float _ClayCost;
    [SerializeField]
    private GameObject _Clay;
    [SerializeField]
    private float boostTime_0 = 0.5f;
    [SerializeField]
    private float boostTime_1 = 0.5f;
    [SerializeField]
    private float boostSpeed_0 = 1f;
    [SerializeField]
    private float boostSpeed_1 = 3f;
    [SerializeField]
    private float _MinRadius;
    [SerializeField]
    private float _MaxRadius;
    [SerializeField]
    internal MinionType _MinionType;
    [SerializeField]
    internal float GolemAttackRadius = 7f;
    [SerializeField]
    internal Animator AnimController;
    [SerializeField]
    private LayerMask _layer;
    [SerializeField]
    private AI_State StunState;

    internal PhotonView PhotonView;
    internal Health TargetPlayerHealth;
    internal NavMeshAgent Agent;
    private Health _Health;
    internal Vector3 PatrolPoint;
    internal bool EnemyAcquired = false;
    internal bool StopMoving = false;
    internal bool AttackEnemy = false;
    internal int EnemyRef;
    private bool AIActive = false;
    internal float DeltaT = 0f;
    internal bool MoveToAttack = false;
    private float _BoostTime;
    private float _BoostSpeed;
    private float _DistanceClamp = 0f;
    internal int AttackCount = 1;
    internal int HPFlee = 80;
    internal bool StunUnit = false;
    internal float StunCD = 0f;
    internal float StunTimer = 0;
    internal float AgentSpeed = 0f;
    internal bool WalkAnim = true;
    internal float CDTimer = 0;

    #endregion GlobalVariables

    #region  LifeCycle

    private void OnEnable()
    {
        StunCD = AttackCD + (AttackCD / 2f);
        Agent = GetComponent<NavMeshAgent>();
        _Health = GetComponent<Health>();
        AIActive = true;
        AIInPLayList.Add(this);
        // ResourcePoolEvents.OnDepletePoolEvent(_ClayCost);
        _BoostSpeed = Random.Range(boostSpeed_0, boostSpeed_1);
        _BoostTime = Random.Range(boostTime_0, boostTime_1);
        StartCoroutine(Tick());

        _Health.OnHealthChange += OnHealthChange;
        AgentSpeed = Agent.speed;
        CDTimer = AttackCD;
        DeltaT = AttackCD;
        StartCoroutine(LifeSpawn());
    }
    private void OnDisable()
    {
        AIInPLayList.Remove(this);
        _Health.OnHealthChange -= OnHealthChange;
        if (AIAttackSystem.AttackingAI.Contains(this))
        {
            AIAttackSystem.AttackingAI.Remove(this);
        }
    }



    private void Update()
    {
        DeltaT = Time.deltaTime;
        StunTimer += Time.deltaTime;
        if (AIActive == false || _CurrentSate == null)
        {
            return;
        }
        else
        {
            _CurrentSate.UpdateState(this);
        }

        // Running animation
        if (WalkAnim == true)
        {
            AnimController.SetBool("IsWalking", true);
        }
        else
        {
            AnimController.SetBool("IsWalking", false);
        }

    }

    #endregion LifeCycle

    

    #region  ClassFunctions

    private void OnHealthChange(float health, bool isHealing)
    {
        // Make Impact animation
        //
        if (health <= 0)
        {
            var obj = Instantiate(_Clay, transform.position, Quaternion.identity);
            ClayRestorer Clay = obj.GetComponent<ClayRestorer>();
            Clay.RestoreAmount = _ClayCost;

            // Death animation
            WalkAnim = false;
            AnimController.SetBool("IsDeath", true);
            StartCoroutine(Die());
        }
    }
    public void ChangeState(AI_State newState)
    {
        if (newState == null)
        {
            return;
        }

        if (_CurrentSate != null)
        {
            _CurrentSate.ExitState(this);
        }

        _CurrentSate = newState;
        _CurrentSate.EnterState(this);
    }


    private IEnumerator Tick()
    {
        while (true)
        {
            _DistanceClamp = Mathf.Clamp(Agent.remainingDistance, 3f, 50f);
            // Agent.radius = MapFloat(_DistanceClamp, 3f, 50f, _MinRadius, _MaxRadius);
            yield return new WaitForSecondsRealtime(_BoostTime);
            Agent.speed += _BoostSpeed;
            yield return new WaitForSecondsRealtime(_BoostTime);
            Agent.speed -= _BoostSpeed;
        }
    }




    #endregion ClassFunctions

    public void SendRPC_Damage()
    {
        PhotonView PlayerPhotonView = TargetPlayerHealth.gameObject.GetComponent<PhotonView>();
        if (PlayerPhotonView != null)
        {
            PlayerPhotonView.RPC("RPC_DamagePlayer", PlayerPhotonView.owner, Damage, false);
            PlayerPhotonView.RPC("RPC_SetMinionType", PhotonTargets.All, _MinionType);
        }
    }

    public void DoAEOAttack()
    {
        Collider[] PlayerInArea = Physics.OverlapSphere(transform.position, GolemAttackRadius, _layer);

        foreach (var player in PlayerInArea)
        {
            PhotonView PlayerPhotonView = player.gameObject.GetComponent<PhotonView>();
            AkSoundEngine.PostEvent("Play_Golem_AEO", gameObject);
            SendRPC_AOEDamage(PlayerPhotonView);
        }

        

    }

    public void SendRPC_AOEDamage(PhotonView photonView)
    {

        photonView.RPC("RPC_DamagePlayer", photonView.owner, AOEDamage, false);
        photonView.RPC("RPC_SetMinionType", PhotonTargets.All, _MinionType);

    }


    private float MapFloat(float val, float min1, float max1, float min2, float max2)
    {
        return (val - min1) * (max2 - min2) / (max1 - min1) + min2;
    }

    public IEnumerator SetAnimWalking ()
    {
        yield return new WaitForSeconds(1f);
        WalkAnim = true;
    }

    private IEnumerator LifeSpawn()
    {
        while (true)
        {
          
            yield return new WaitForSeconds(60f);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private IEnumerator Die()
    {
        _CurrentSate = StunState;
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(gameObject);
    }

}
