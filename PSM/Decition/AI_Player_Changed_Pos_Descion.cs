using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Player_Changed_Pos")]
public class AI_Player_Changed_Pos_Descion : AI_Decision {
    public override bool MakeDecision(AIUnit unit)
    {
      bool EnemyProximity = ProximityToEnemy(unit);
	  return EnemyProximity;
    }

   private bool ProximityToEnemy(AIUnit unit)
    {
		
		if(unit.TargetPlayerHealth != null)
		{
			if(Vector3.Distance( unit.Agent.transform.position, unit.TargetPlayerHealth.transform.position) >= unit.Agent.remainingDistance)
			{
				unit.Agent.SetDestination( unit.TargetPlayerHealth.transform.position);
				return true;
			}
			else
			{
		
				return false;
			}
		}
		return false;
	
    }
}
