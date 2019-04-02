using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Stun_Decsion")]
public class Ai_Stun_Decision : AI_Decision
{
    public override bool MakeDecision(AIUnit unit)
    {
        bool Stun = StunUnit(unit);
		return Stun;
    }

   private bool StunUnit (AIUnit unit)
	{
		if(unit.StunUnit == true && unit.StunTimer <= unit.StunCD  )
		{
			return true;
		}
		return false;
	}
}
