using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "PluggbleAI/Reset_StopingDistance")]
public class AI_RestStopingDistance : AI_Actions {
    public override void UnitAction(AIUnit unit)
    {
		  unit.Agent.stoppingDistance = 20;
    }
}
