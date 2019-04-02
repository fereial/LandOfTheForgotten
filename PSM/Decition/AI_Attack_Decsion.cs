using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Attack_Decsion")]
public class AI_Attack_Decsion : AI_Decision
{
    public override bool MakeDecision(AIUnit unit)
    {
      bool Attack = AttackInRange(unit);
			return Attack;
    }

    private bool AttackInRange(AIUnit unit)
    {
			if(unit.TargetPlayerHealth != null && unit.TargetPlayerHealth.MyHealth > 0 )
			{
				if(Vector3.Distance(unit.transform.position, unit.TargetPlayerHealth.transform.position) <= unit.AgentStopDistance)
				{ 
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
    }
}
