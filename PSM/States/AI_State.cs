using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggbleAI/State")]
public class AI_State : ScriptableObject 
{
	#region GlobalsVariables
		
		[SerializeField]
		private AI_Actions[] _EnterActions;

		[SerializeField]
		private AI_Actions[] _UpdateActions;

		[SerializeField]
		private AI_Actions[] _ExitActions;

		[Space, Tooltip("All Transitions are OR-ed"), SerializeField]
		private Transition[] _Transitions;

		protected Color _debugColor = Color.grey;
	#endregion GlobalsVariables

	
		public void EnterState(AIUnit unit)
		{
			DoActions(_EnterActions, unit);
		}

		public void UpdateState(AIUnit unit)
		{
			DoActions(_UpdateActions, unit);
			CheckTransitions(unit);
		}

		public void ExitState(AIUnit unit)
		{
			DoActions(_ExitActions, unit);
		}

		private void DoActions(AI_Actions[] actionsToDo, AIUnit unit)
		{
			foreach (var item in actionsToDo)
			{
				item.UnitAction(unit);
			}
		}

		private void CheckTransitions(AIUnit unit)
		{
			foreach (var item in _Transitions)
			{
				item.CheckAndTransition(unit);
			}
		}


	

}
