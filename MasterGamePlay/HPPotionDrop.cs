using UnityEngine;

public class HPPotionDrop : MonoBehaviour 
{
	
	[SerializeField]
	private GameObject _HPPrefab;
	private Health _MyHealth;

	private void OnEnable()
	{
		_MyHealth = GetComponent<Health>();
		_MyHealth.OnHealthChange += OnHealthChange;
	}

	private void OnDisable()
	{
		_MyHealth.OnHealthChange -= OnHealthChange;
	}



    private void OnHealthChange(float health, bool isHealing)
    {
        if (health <= 0)
        {
			if(PhotonNetwork.isMasterClient == false)
			{
				int RandomChance = Random.Range(1,100);
				if(RandomChance >= 60)
				{
					Vector3 Pos = transform.position;
					Pos.y = 0.8f;  
            		Instantiate(_HPPrefab, Pos , Quaternion.identity);
			
				}

			}
           
        }
    }
	
}
