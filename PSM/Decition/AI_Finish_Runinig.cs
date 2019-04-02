using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Stop_Runing_Decion")]
public class AI_Finish_Runinig : AI_Decision 
{   
	public override bool MakeDecision(AIUnit unit)
    {
       bool stop = GoalReached(unit);
	   return stop;
    }

	private bool GoalReached(AIUnit unit )
	{
		if(unit.Agent.remainingDistance <= unit.Agent.stoppingDistance && unit.Agent.pathPending == false )
		{
			return true;
		}

		return false;
	}

}


