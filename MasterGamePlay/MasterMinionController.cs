// ======================================================================
//    Author    	 : Eial Ferencz Appel 
//    Version        : 1.0
//    Program        : Unity 2018.2.18f1
// 	  (C) Copyright 2019 All rights reserved.
// ======================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MasterMinionController : MonoBehaviour 
{
	#region  GlobalVariables

		private List<AIUnit> _UnitSelected = new List<AIUnit>();
		[SerializeField]
		private LayerMask _MinionLayer;
		[SerializeField]
		private LayerMask _PlaneLayer;

		private Camera _MyCam;
		private MasterObjectSpawner _MinionSpawner;
		private Vector2 _InitialMousePos;
		private Vector2 _FinalMousePos;
		private RaycastHit _RayHit;

	#endregion GlobalVariables

	#region LifeCycle

		private void Awake()
		{
			_MyCam = GetComponentInChildren<Camera>();
			_MinionSpawner = GetComponent<MasterObjectSpawner>();
		}

		private void Update()
		{
	
			if(Input.GetMouseButtonDown(0))
			{
				_InitialMousePos = Input.mousePosition;
			}
			

			if(Input.GetMouseButtonUp(0))
			{
				// DoRay();
				_FinalMousePos = Input.mousePosition;
				MultipleMinionSelection();
			}

			if(Input.GetMouseButtonDown(1))
			{

				GiveMinionDirection();
			}
		}


    #endregion LifeCycle

    #region ClassFunctions
 
		private void GiveMinionDirection()
		{
			Ray ray =_MyCam.ScreenPointToRay(Input.mousePosition);
			Vector3 MinionDestination = Vector3.zero;
			RaycastHit DirectionHit;
			if(Physics.Raycast(ray, out DirectionHit , 10000, _PlaneLayer))
			{

				MinionDestination = DirectionHit.point;
			}

			foreach(var item in _UnitSelected)
			{
				item.Agent.SetDestination(MinionDestination);
			}
			_UnitSelected.Clear();
		}

		private void MultipleMinionSelection()
		{
			_UnitSelected.Clear();
			//Debug.Log( AIUnit.AIInPLayList.Count);
			foreach (var item in AIUnit.AIInPLayList)
			{
				Vector2 ScreenPos = _MyCam.WorldToScreenPoint(item.transform.position);
				float MinInitialX = Mathf.Abs(_InitialMousePos.x);
				float MinInitialY = Mathf.Abs(_InitialMousePos.y);
				float MaxFinalX = Mathf.Abs(_FinalMousePos.x);
				float MaxFinalY = Mathf.Abs(_FinalMousePos.y);
				
				if(Mathf.Abs( ScreenPos.x) >= MinInitialX && Mathf.Abs( ScreenPos.x ) <= MaxFinalX && Mathf.Abs( ScreenPos.y ) <=MinInitialY  && Mathf.Abs( ScreenPos.y )>= MaxFinalY ||
					Mathf.Abs( ScreenPos.x ) <= MinInitialX && Mathf.Abs( ScreenPos.x )>= MaxFinalX && Mathf.Abs( ScreenPos.y ) >=MinInitialY  && Mathf.Abs( ScreenPos.y ) <= MaxFinalY)
				{
					_UnitSelected.Add(item);
				}
				//Debug.Log(_UnitSelected.Count);
			}

		}

	#endregion ClassFunctions
}