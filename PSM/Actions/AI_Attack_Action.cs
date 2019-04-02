// ======================================================================
//    Author    	 : Eial Ferencz Appel 
//    Version        : 1.0
//    Program        : Unity 2018.2.18f1
// 	  (C) Copyright 2019 All rights reserved.
// ======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Attack_Action")]
public class AI_Attack_Action : AI_Actions
{
    [SerializeField]
    private LayerMask _layer;

    public override void UnitAction(AIUnit unit)
    {
        DoAttack(unit);
    }

    private void DoAttack(AIUnit unit)
    {
        unit.CDTimer += unit.DeltaT;
        if (unit.TargetPlayerHealth == null)
        {
            return;
        }

        if (Vector3.Distance(unit.transform.position, unit.TargetPlayerHealth.transform.position) < unit.AttackRange)
        {
            unit.WalkAnim = false;
            if (unit.CDTimer >= unit.AttackCD && unit.AttackEnemy == true)
            {
                if (unit.TargetPlayerHealth != null && unit.TargetPlayerHealth.MyHealth > 0)
                {

                    if (unit._MinionType == MinionType.Ghost && unit.TargetPlayerHealth != null)
                    {
                        unit.Agent.transform.LookAt(unit.TargetPlayerHealth.transform.position);
                        MinionRangeAttack Attack = unit.GetComponent<MinionRangeAttack>();
                        if (unit.TargetPlayerHealth != null)
                        {
                            unit.AnimController.SetTrigger("IsAttacking");
                            Attack.RangeAttack(unit.TargetPlayerHealth.transform.position);
                            unit.CDTimer = 0;
                        }
                    }

                    else if (unit._MinionType == MinionType.Golem && unit.TargetPlayerHealth != null && unit.CDTimer >= unit.AttackCD)
                    {
                        unit.CDTimer = 0;
                        unit.WalkAnim = false;
                        unit.Agent.transform.LookAt(unit.TargetPlayerHealth.transform.position);
                        unit.AttackCount = (unit.AttackCount + 1) % 3;
                        unit.AnimController.SetInteger("AttackState", unit.AttackCount);
                        unit.AnimController.SetTrigger("IsAttacking");
                        unit.StartCoroutine(unit.SetAnimWalking());
                    }
                    else if(unit._MinionType == MinionType.Mummy && unit.TargetPlayerHealth != null)
                    {
                        unit.Agent.transform.LookAt(unit.TargetPlayerHealth.transform.position);

                        // Make a random attack animation
                        int randomAttack = Random.Range(1, 3);
                        unit.AnimController.SetInteger("AttackState", randomAttack);
                        unit.AnimController.SetTrigger("IsAttacking");

                        // unit.SendRPC_Damage();
                      unit.StartCoroutine(unit.SetAnimWalking());
                        unit.CDTimer = 0;
                    }
                }
                else
                {
                    unit.TargetPlayerHealth = null;
                }

            }
        }
        
    }
}
