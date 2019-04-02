using UnityEngine;

public class ClayRestorer : MonoBehaviour, IPoolRechargeable
{
	[SerializeField]
	private FloatVar _ClayPoolAmount;
	
	private  float _ClayAmount;

	public float RestoreAmount
	{
		get 
		{
			return _ClayAmount;
		}   
		set
		{
			_ClayAmount = value;
		}
		
	}

	public void RechargePool()
	{
		
		_ClayPoolAmount.Value += _ClayAmount;
		if(	_ClayPoolAmount.Value >=  MasterResourceController.InitialClayPool )
		{
			_ClayPoolAmount.Value = MasterResourceController.InitialClayPool;
		}
		AkSoundEngine.PostEvent("Play_Clay_Pickup", gameObject);
		Destroy(gameObject);
	}

	// private void OnMouseOver()
	// {
	// 	RechargePool();
	// }

	// private void OnMouseUpEnter()
	// {
	// 	RechargePool();
	// }
}
