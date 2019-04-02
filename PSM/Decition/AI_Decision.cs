using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Decision : ScriptableObject
{

	// makes the decision to change or not to change state
	public abstract bool MakeDecision( AIUnit unit );

}
