using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Player_In_Range_Actions")]
public class AI_PLayerInRange_Action : AI_Actions {

    public override void UnitAction(AIUnit unit)
    {
       EnemyInRange(unit);
    }

    private void EnemyInRange(AIUnit unit)
    {

		if(unit.TargetPlayerHealth != null )
		{
     
			if(Vector3.Distance( unit.Agent.transform.position, unit.TargetPlayerHealth.transform.position) >= unit.AttackRange)
			{
        unit.Agent.isStopped = false;
        PhotonView MyPhoton = unit.gameObject.GetComponent<PhotonView>();
        if(MyPhoton.isMine)
        {
				  unit.Agent.SetDestination(unit.TargetPlayerHealth.transform.position);
          unit.Agent.transform.LookAt(unit.TargetPlayerHealth.transform.position);
        }
			
			}
		}
        
    }
}
