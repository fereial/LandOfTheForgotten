using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Kepp_Attacking_Descicon")]
public class KeepAttacking_Decsion : AI_Decision 
{
    public override bool MakeDecision(AIUnit unit)
    {
       bool Attacking = KeepAttacking(unit);
	   return Attacking; 

    }

    private bool KeepAttacking(AIUnit unit)
    {
        if(unit.TargetPlayerHealth !=null)
		{
			if(unit.TargetPlayerHealth.MyHealth > 0 )
			{
				return true;
			}
			else
			{
				unit.TargetPlayerHealth = null;
				return false;		
			}
		}
		else
		{
			return false;
		}
    }
}
