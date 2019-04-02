using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Patrol_Action")]
public class Patrol_Action : AI_Actions
{
	#region AbstractFunctions

		public override void UnitAction(AIUnit unit)
		{
			LookForPatrol(unit);
		}

	#endregion AbstractFunctions

	#region BehaviorFunctions
		private void LookForPatrol(AIUnit unit)
		{
			// get the postion of the patrol to move
			if(unit.Agent.remainingDistance <= unit.Agent.stoppingDistance && unit.Agent.pathPending == false )
			{
				var tmp = NextPatrol();

				unit.PatrolPoint = FindPoint(tmp.transform.position, 5);
				unit.Agent.SetDestination(unit.PatrolPoint);
				
			}
		}

		private PatrolPoint NextPatrol ()
		{	// select a random patrol point
			return PatrolPoint.PatrolList[Random.Range( 0 , PatrolPoint.PatrolList.Count )];
		}

		private Vector3 FindPoint(Vector3 c, float r)
		{
			int RandomAngle = Random.Range( 0 , 360);
			Vector3 PosRef =  c + Quaternion.AngleAxis(RandomAngle, Vector3.forward) * (Vector3.right* r );
			return PosRef;
		}
		
		
	#endregion BehaviorFunctions

}
