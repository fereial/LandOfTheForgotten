using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "PluggbleAI/Change_Stoping_Distance")]
public class AI_ChangeStopingDistance_Act : AI_Actions
{
    public override void UnitAction(AIUnit unit)
    {
       unit.Agent.stoppingDistance = unit.AgentStopDistance;
    }
}
