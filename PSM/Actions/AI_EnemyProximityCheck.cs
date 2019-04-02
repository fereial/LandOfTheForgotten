using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Enemy_Proximity_Action")]
public class AI_EnemyProximityCheck : AI_Actions
{
	[SerializeField]
	private LayerMask _layerMask;

    public override void UnitAction(AIUnit unit)
    {
		CheckForDistance(unit);
    }

    private void CheckForDistance(AIUnit unit)
    {
		if(unit.EnemyAcquired == true )
		{
			unit.MoveToAttack = true;
		}

		// foreach (var item in PlayerNetworkManager.PlayerStats)
		// {
		// 	if(item.MyHealth > 0 )
		// 	{
		// 		float DistRef = 1000000;
		// 		float DistanceToPlayer = Vector3.Distance(unit.transform.position, item.transform.position );
		// 		Vector3 PlayerRef = Vector3.zero;
		// 		if( DistanceToPlayer < unit.EnemyDetectionRadius && DistanceToPlayer < DistRef)	
		// 		{	
		// 			DistRef = DistanceToPlayer;
		// 			unit.TargetPlayerHealth = item;
		// 			unit.EnemyAcquired = true;
		// 			unit.MoveToAttack = true;	
				
		// 		}
		// 	}
		// }
	

		// Collider[] EnemiesInArea =  Physics.OverlapSphere(unit.transform.position, unit.EnemyDetectionRadius, _layerMask);
		// Debug.Log(EnemiesInArea.Length);
		// int IndexRef = 0;
		// if(EnemiesInArea.Length > 0)
		// {
		// 	float DistanceRef = 500000;
		// 	for(int i= 0; i < EnemiesInArea.Length ; i++)
		// 	{
		// 		float Distance = Vector3.Distance(unit.transform.position, EnemiesInArea[i].GetComponent<Health>().transform.position);
		// 		if(Distance < DistanceRef)
		// 		{
		// 			IndexRef = i;
		// 		}
		// 	}
			
		// 	Health PlayerHP =  unit.EnemiesInArea[IndexRef].GetComponent<Health>();
		// 	unit.PlayerHealth  = PlayerHP;
		// 	unit.EnemyAcquired = true;	
		// }
    }
}