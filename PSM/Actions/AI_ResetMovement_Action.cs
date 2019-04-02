
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/RestMoving_Moving")]
public class AI_ResetMovement_Action : AI_Actions
{
    public override void UnitAction(AIUnit unit)
    {
        KeepMoving(unit);
    }
	  private void KeepMoving(AIUnit unit)
    {
      //Debug.Log("Exit attack state");
      unit.Agent.speed = unit.AgentSpeed;
      unit.Agent.isStopped = false;
    }
}
