using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/AI_Run_Away_Action")]
public class AI_RUn_Away_Action : AI_Actions
{
    public override void UnitAction(AIUnit unit)
    {
       RunAway( unit);
    }

	private void RunAway(AIUnit unit)
	{
		Vector3 Pos = unit.transform.position -(new Vector3(1, 0 , 1 )* -5);
		unit.Agent.SetDestination(Pos);

	}
}
