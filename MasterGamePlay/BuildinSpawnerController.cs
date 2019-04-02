// ======================================================================
//    Author    	 : Eial Ferencz Appel 
//    Version        : 1.0
//    Program        : Unity 2018.2.18f1
// 	  (C) Copyright 2019 All rights reserved.
// ======================================================================

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum MinionType
{
	Mummy = 0,
	Ghost,
	Golem
}

public class BuildinSpawnerController : MonoBehaviour 
{
	#region  GlobalVariables
		[SerializeField]
		private GameObject _SpawnRef;
		[SerializeField]
		private float _SpawnCoolDown;
		[SerializeField]
		public MinionType _MinionTypeToSpawn;	
		[SerializeField]
		private float _ClayCost = 10f;
		[SerializeField]
		private FloatVar _CurrentClayAmount;
		[SerializeField]
		private GameObject _Clay;
		[SerializeField]
		private int _SpawnCounter = 2;
		[SerializeField]
		private float _MinionToSpawn = 5;
		private float _CoolDownTimer;
		private Health _Health;
		private int _Count = 0;
		private float _InitialClayCost = 10f;
		public Color AreaColor = Color.red;

	#endregion GlobalVariables

	#region LifeCycle

		private void OnEnable()
		{
			_InitialClayCost = _ClayCost;
			_ClayCost = _ClayCost * _MinionToSpawn;
			
			ResourcePoolEvents.OnDepletePoolEvent(_ClayCost);
			_Health = GetComponent<Health>();
			_Health.OnHealthChange += OnHealthChange;
			_CoolDownTimer = _SpawnCoolDown/2f;
			AkSoundEngine.PostEvent("Play_Llorona_Build", gameObject);
		}

		private void OnDisable()
		{
        	_Health.OnHealthChange -= OnHealthChange;
   	 	}

	
		private void OnHealthChange(float health, bool isHealing)
     	{
			if (health <= 0)
			{
				
				var obj = Instantiate(_Clay, transform.position, Quaternion.identity);
				ClayRestorer Clay = obj.GetComponent<ClayRestorer>();
				Clay.RestoreAmount = _InitialClayCost;
			}
    	}

		private void Update()
		{
			_CoolDownTimer += Time.deltaTime;
			if(PhotonNetwork.isMasterClient)
			{
				SpawnMinion();
			}

			if(_Count >= _MinionToSpawn )
			{
				_Health.MyHealth = 0f;
				PhotonNetwork.Destroy(gameObject);
			}
		}



	#endregion LifeCycle

	#region ClassFunctions

		private void SpawnMinion()
		{

			if(_CoolDownTimer > _SpawnCoolDown && _Count < _MinionToSpawn)
			{
				Vector3 Pos = _SpawnRef.transform.position;
				for(int i = 0; i < _SpawnCounter; i++)
				{
					Pos.x += Random.Range(-5,5);
					Pos.z += Random.Range(-5,5);
					
					if(_MinionTypeToSpawn == MinionType.Mummy )
					{	
						PhotonNetwork.Instantiate(Path.Combine("Prefabs","Mommy"), Pos , Quaternion.identity, 0);
					}

					if(_MinionTypeToSpawn == MinionType.Golem )
					{
						PhotonNetwork.Instantiate(Path.Combine("Prefabs","Golem"), Pos , Quaternion.identity, 0);
					}
						if(_MinionTypeToSpawn == MinionType.Ghost )
					{
						PhotonNetwork.Instantiate(Path.Combine("Prefabs","Ghost"), Pos , Quaternion.identity, 0);
					}
					_CoolDownTimer = 0;
					++_Count;	
				}
			}
		}


		public float GetCost()
		{
			return _ClayCost * _MinionToSpawn;
		}

	#endregion ClassFunctions
}