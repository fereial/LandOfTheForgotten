using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggbleAI/Move_To_Player_Action")]
public class Move_To_Player_Action : AI_Actions
{
	#region AbstractFunctions

		public override void UnitAction(AIUnit unit)
		{
			MoveToPlayer(unit);
		}

    #endregion AbstractFunctions

	#region BehaviorFunctions

		private void MoveToPlayer(AIUnit unit)
		{
			if(unit.TargetPlayerHealth != null)
			{
			
				unit.Agent.SetDestination(unit.TargetPlayerHealth.transform.position);
				
			}
			else
			{
				PatrolPoint NextPatrol =  SelectPatrolPoint();
				unit.Agent.SetDestination(NextPatrol.transform.position);
				
			}
		}

    private PatrolPoint SelectPatrolPoint()
    {
		return PatrolPoint.PatrolList[Random.Range( 0 , PatrolPoint.PatrolList.Count )];
    }

    #endregion BehaviorFunctions

}
