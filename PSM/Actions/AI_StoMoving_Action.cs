using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu (menuName = "PluggbleAI/Stop_Moving")]
public class AI_StoMoving_Action : AI_Actions
{
    public override void UnitAction(AIUnit unit)
    {
        StopMoving(unit);
    }

    private void StopMoving(AIUnit unit)
    {
			unit.Agent.speed = 0f;
			
		}
}
