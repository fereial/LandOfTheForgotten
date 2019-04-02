using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Transition
{
	[Tooltip("All Decisions are AND-ed"), SerializeField]
	private AI_Decision[] _decisions;

	[SerializeField]
	private AI_State _StateToTransitionIfTrue;

	public void CheckAndTransition(AIUnit unit)
	{
		foreach (var item in _decisions)
		{
			if(item.MakeDecision(unit) == false)
			{
				return;
			}
		}
		unit.ChangeState(_StateToTransitionIfTrue);
	}
	
}
