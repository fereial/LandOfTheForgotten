using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Player_Is_Death_Descicon")]
public class Player_Is_Death_Decision : AI_Decision {
   public override bool MakeDecision(AIUnit unit)
    {
       bool Attacking = PlayerIsDead(unit);
	   return Attacking; 

    }

    private bool PlayerIsDead(AIUnit unit)
    {
        if(unit.TargetPlayerHealth == null || unit.TargetPlayerHealth.MyHealth <= 0 )
		{
            unit.TargetPlayerHealth = null;
			return true;
		}
		else
		{
			return false;
		}
    }
}
