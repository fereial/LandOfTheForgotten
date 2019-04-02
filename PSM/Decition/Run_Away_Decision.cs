
using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/RunAway_Decsion")]
public class Run_Away_Decision : AI_Decision
{
	public override bool MakeDecision(AIUnit unit)
	{
		bool Run = RunAway(unit);
		return Run;
	}

	private bool RunAway(AIUnit unit)
	{
		if(unit.StunUnit == true && unit.StunTimer >= unit.StunCD  )
		{
			unit.StunTimer = 0f;
			unit.StunUnit = false;
			return true;
		}
		return false;
	}

}
