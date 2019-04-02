using UnityEngine;
[CreateAssetMenu(menuName = "PluggbleAI/Look_For_Player_Descicon")]
public class AI_LookForPlayer : AI_Decision
{
    #region GlobalsVariables

    #endregion GlobalsVariables

    public override bool MakeDecision(AIUnit unit)
    {
        // tell if  an enemy is in range
        bool EnemyInRange = SearchForEnemy(unit);
        return EnemyInRange;
    }

    private bool SearchForEnemy(AIUnit unit)
    {
			//seraphs for enemies in are in range
			if(unit.TargetPlayerHealth != null && unit.TargetPlayerHealth.MyHealth > 0)
			{
				return true;
			}
			
			return false;

    }

}
