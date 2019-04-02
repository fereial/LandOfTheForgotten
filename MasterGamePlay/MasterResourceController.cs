// ======================================================================
//    Author    	 : Eial Ferencz Appel 
//    Version        : 1.0
//    Program        : Unity 2018.2.18f1
// 	  (C) Copyright 2019 All rights reserved.
// ======================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterResourceController : MonoBehaviour 
{
	#region  GlobalVariables
		[SerializeField]
		private float _InitialClayPool = 300f; 
		public static float InitialClayPool = 300f;
		[SerializeField]
		private LayerMask _Layer;
		[SerializeField]
		private float _AutoRegen = 0.5f;
		[SerializeField]
		private FloatVar _CurrentClayPool;
		[SerializeField]
		private float _CD = 50f;
		private float _CDTimer = 0f;
	

	#endregion GlobalVariables


	#region  LifeCycle
		private void OnEnable()
		{
			InitialClayPool = _InitialClayPool;
			ResourcePoolEvents.RechargePoolEvent += RestoreClayPool;
			ResourcePoolEvents.DepletePoolEvent += ReduceClayPool;
		}

		private void OnDisable()
		{
			ResourcePoolEvents.RechargePoolEvent -= RestoreClayPool;
			ResourcePoolEvents.DepletePoolEvent -= ReduceClayPool;
		}

		private void Awake()
		{
			_CurrentClayPool.Value = InitialClayPool;
		}
		
		private void Update()
		{
			_CDTimer += Time.deltaTime;
			if(_CurrentClayPool.Value < InitialClayPool && _CDTimer >= _CD  )
			{
			 	if(_CurrentClayPool.Value + _AutoRegen >= InitialClayPool )
				{
					_CurrentClayPool.Value = InitialClayPool;
				} else
				{
					_CurrentClayPool.Value += _AutoRegen;
				}
				
				_CDTimer = 0;
			}
		}



	#endregion LifeCycle

	#region  ClassFunctions

		public float GetCurrentClayPool()
		{
			return _CurrentClayPool.Value;
		}

		public void ReduceClayPool( float Clay )
		{
			if(_CurrentClayPool.Value - Clay <= 0 )
			{
				_CurrentClayPool.Value = 0;
			}
			else
			{
				_CurrentClayPool.Value -= Clay;
				
			}
		
		}

		public void RestoreClayPool(float Clay)
		{
			
			if(_CurrentClayPool.Value + Clay >=  InitialClayPool )
			{
				_CurrentClayPool.Value = InitialClayPool;
				
			}
			else
			{
				_CurrentClayPool.Value += Clay;
			}
		}

	#endregion ClassFunctions 
}
