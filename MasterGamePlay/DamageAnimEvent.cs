using UnityEngine;

public class DamageAnimEvent : MonoBehaviour 
{
	private AIUnit _Unit;
	
	private void OnEnable()
	{
		_Unit = GetComponentInParent<AIUnit>();
	}

	public void AnimDamage()
	{
		if(_Unit != null)
		{
			_Unit.SendRPC_Damage();
		}
	}

	public void AOEAnimDamage()
	{
		_Unit.DoAEOAttack();
	}


	
}
