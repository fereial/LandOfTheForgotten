using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Player_In_Range_Decision")]
public class AI_PLayeIsInRangeForAttack: AI_Decision 
{
	public override bool MakeDecision(AIUnit unit)
	{
		bool CheckForrange = PlayerOutOFAttackRange(unit);
		return CheckForrange;
	}

	private bool PlayerOutOFAttackRange(AIUnit unit) 
	{
		if (unit.TargetPlayerHealth != null) 
		{
			//Debug.Log(Vector3.Distance(unit.Agent.transform.position, unit.TargetPlayerHealth.transform.position));
			if (Vector3.Distance(unit.Agent.transform.position, unit.TargetPlayerHealth.transform.position) >= unit.AttackRange) 
			{
				unit.Agent.isStopped = false;
				PhotonView MyPhoton = unit.gameObject.GetComponent < PhotonView > ();
				if (MyPhoton.isMine) 
				{
					unit.Agent.SetDestination(unit.TargetPlayerHealth.transform.position);
					return true;
				}

			}
		}
		return false;
	}
}