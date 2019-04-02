using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISelection : MonoBehaviour
{
	[SerializeField]
	private Camera _MyCam;
	[SerializeField]	
	private RectTransform _SelectSquare;

	private Vector3 _StartPos;
	private Vector3 _EndPos;
	private	Vector3 _SquareStart;
	private bool _GamePause = false;
	private void Start()
	{
		_SelectSquare.gameObject.SetActive(false);
	}

	private void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(_GamePause == false )
			{
				_GamePause = true;
			}
			else
			{
				_GamePause = false;
			}
		}

		if(_GamePause == false )
		{
			Drawbox();
		}
	}

	private void Drawbox()
	{
			if(Input.GetMouseButtonDown(0))
		{
			Ray ray =_MyCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit RayHit;

			if(Physics.Raycast(ray, out RayHit))
			{
				_StartPos = RayHit.point;
			 	_SquareStart = _MyCam.WorldToScreenPoint(_StartPos);
				_SquareStart.z = 0;
				
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			_SelectSquare.gameObject.SetActive(false);		
		}

		if(Input.GetMouseButton(0))
		{
			if(_SelectSquare.gameObject.activeInHierarchy == false ) 
			{
				_SelectSquare.gameObject.SetActive(true);		
			}
			_EndPos = Input.mousePosition;

			Vector3 Center = (_SquareStart + _EndPos) /2f;
			_SelectSquare.position = Center;

			float sizeX = Mathf.Abs(_SquareStart.x - _EndPos.x);
			float sizeY = Mathf.Abs(_SquareStart.y - _EndPos.y);

			_SelectSquare.sizeDelta = new Vector2(sizeX, sizeY);
		}
	}

}