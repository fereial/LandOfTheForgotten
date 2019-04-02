using System;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Player_In_Range")]
public class AI_PlayerInRange : AI_Decision
{
	
    public override bool MakeDecision(AIUnit unit)
    {
        bool EnemyProximity = ProximityToEnemy(unit);
		return EnemyProximity;
    }

    private bool ProximityToEnemy(AIUnit unit)
    {
		if(unit.MoveToAttack == true)
		{
			if(unit.TargetPlayerHealth != null)
			{
				if(Vector3.Distance( unit.Agent.transform.position, unit.TargetPlayerHealth.transform.position) <= unit.Agent.remainingDistance)
				{
					unit.StopMoving = true;
					return true;
				}
				else
				{
					unit.StopMoving = false;
					return false;
				}
			}
		}
		return false;
    }
}
