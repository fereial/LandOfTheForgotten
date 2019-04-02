using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MasterObjectSpawner: MonoBehaviour 
{
	public static List<BuildingInPLayList> BuildingInPlay = new List<BuildingInPLayList>();
	
	#region GlobalVariables
	[SerializeField]
	private GameObject _MommyPrefab;
	[SerializeField]
	private GameObject _GhostPrefab;
	[SerializeField]
	private GameObject _GolemPrefab;
	[SerializeField]
	private GameObject _LightingPrefab;
	[SerializeField]
	private float _SpawnCoolDown = 0f;
	[SerializeField]
	private float _CoolDownTimer;
	[SerializeField]
	private GameObject _MummyBuilding;
	[SerializeField]
	private GameObject _GolemBuilding;
	[SerializeField]
	private GameObject _GhostBuilding;
	[SerializeField]
	private GameObject _Lighting;
	[SerializeField]
	private FloatVar _CurrentClayPoolAmount;
	[HideInInspector]
	public bool SpawnMinionPrefab = false;
	[SerializeField]
	private PhotonView _PhotonView;
	
	private int _Layer;
	[SerializeField, Range(0, 100)]
	private float _RotationSpeed;
	[HideInInspector]
	public static bool AbleToBuild = false;

	private GameObject _MinionBuildingToSpawn;
	private int _MinionCountToSpawn = 0;
	private Camera _MasterCamera;
	private bool _SpawnMommy = false;
	private bool _SpawnGhost = false;
	private bool _SpawnGolem = false;
	private bool _SpawnLighting = false;
	private bool _ShowRef = false;
	private GameObject _BuildingRef;
	private float _MouseWheelRotation;
	private Quaternion _QuaternionRef = Quaternion.identity;
	private BuildinSpawnerController _MummySpawner;
	private BuildinSpawnerController _GhostSpawner;
	private BuildinSpawnerController _GolemSpawner;
	private MasterLightingDmg _LightingCost;
	

	#endregion GlobalVariables

	# region LifeCycle
	private void Awake() 
	{
		_SpawnMommy = true;
		_MasterCamera = GetComponentInChildren < Camera > ();
		_CoolDownTimer = 100000;
		if (_PhotonView.isMine == true) {
			_MasterCamera.enabled = true;
		} else {
			_MasterCamera.enabled = false;
		}
		_MummySpawner = _MummyBuilding.GetComponent<BuildinSpawnerController>();
		_GhostSpawner = _GhostBuilding.GetComponent<BuildinSpawnerController>();
		_GolemSpawner = _GolemBuilding.GetComponent<BuildinSpawnerController>();
		_LightingCost = _Lighting.GetComponent<MasterLightingDmg>();
		AbleToBuild = false;

		_Layer = LayerMask.GetMask("Terrian", "InvalidBuildingPlace");

    }

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) 
		{
			SpawnMummy();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) 
		{
			SpawnGhost();
		}

		if (Input.GetKeyDown(KeyCode.Alpha3)) 
		{
			SpawnGolem();
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) 
		{
			SpawnLighting();
		}


		BuildingSpawnRef();
		RotateBuilding();

		_CoolDownTimer += Time.deltaTime;
		if (_CoolDownTimer >= _SpawnCoolDown) 
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				_ShowRef = false;
				SpawnBuilding();
				
			}
		}
		else
		{
			
		}

		if(_BuildingRef != null)
		{
			if(Input.GetMouseButtonDown(1))
			{
				DereferenceSpawnObj();
			}
		}
	}

	# endregion LifeCycle

	# region ClassFunctions

	private void SpawnBuilding() 
	{
		Destroy(_BuildingRef);
		_ShowRef = true;
		if (SpawnMinionPrefab == true ) 
		{
			int Count = 0;
			Ray ray = _MasterCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit RayHit;
		
			if (Physics.Raycast(ray, out RayHit, 10000 , _Layer) && AbleToBuild == true && _CurrentClayPoolAmount.Value > 0)
			{
                if (RayHit.transform.gameObject.layer == LayerMask.NameToLayer("InvalidBuildingPlace"))
                {
                    //fail!!!
                    AkSoundEngine.PostEvent("Play_Cant_Place", gameObject);
                    SpawnMinionPrefab = false;
                    AbleToBuild = false;
                    return;
                }

				//Debug.Log("@@@@@@   Layer  @@@@@"+ RayHit.transform.gameObject.layer);
				Vector3 Pos = RayHit.point;
				Pos.y = 2f;
				if (_SpawnMommy == true   && _CurrentClayPoolAmount.Value > _MummySpawner.GetCost()) 
				{
					Pos.y = 1.5f;
					GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Test", "MummyBuilding"), Pos, Quaternion.identity, 0);
					AkSoundEngine.PostEvent("Play_Mummy", gameObject);
					_CoolDownTimer = 0;
				}

				if (_SpawnGhost == true && _CurrentClayPoolAmount.Value > _GolemSpawner.GetCost()) 
				{
					Pos.y = 1f;
					GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Test", "GosthBUilding"), Pos, Quaternion.identity, 0);
					AkSoundEngine.PostEvent("Play_Ghost", gameObject);
					_CoolDownTimer = 0;
				}

				if (_SpawnGolem == true && _CurrentClayPoolAmount.Value >_GhostSpawner.GetCost() )  
				{
					Pos.y = 2f;
					GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Test", "GloemBuilding"), Pos, Quaternion.identity, 0);
					AkSoundEngine.PostEvent("Play_Golem", gameObject);
					_CoolDownTimer = 0;
				}

				if (_SpawnLighting == true && _CurrentClayPoolAmount.Value > _LightingCost.GetCost() ) 
				{
					Pos.y = 0;
					GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Test", "Lighting"), Pos, Quaternion.identity, 0);
					_CoolDownTimer = 0;
				}
				
				
			}
			else if(SpawnMinionPrefab == false)
			{
				AkSoundEngine.PostEvent("Play_Cant_Place", gameObject);
			}
			else if(_CurrentClayPoolAmount.Value < _MummySpawner.GetCost() || _CurrentClayPoolAmount.Value < _GolemSpawner.GetCost() 
			|| _CurrentClayPoolAmount.Value <_GhostSpawner.GetCost() || _CurrentClayPoolAmount.Value < _LightingCost.GetCost() )
			{
				AkSoundEngine.PostEvent("Play_Not_Enough", gameObject);
			}

			
		}
		SpawnMinionPrefab = false;
		AbleToBuild = false;
	}

	private void BuildingSpawnRef() 
	{
		if (_ShowRef == true && _BuildingRef != null) 
		{
			Ray ray = _MasterCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit RayHit;

			if (Physics.Raycast(ray, out RayHit, 10000, _Layer)) 
			{
				Vector3 Pos = RayHit.point;
				_BuildingRef.transform.position = RayHit.point;
				_BuildingRef.transform.rotation = Quaternion.FromToRotation(Vector3.up, RayHit.normal);

			}
		}
	}

	private void RotateBuilding() 
	{
		_MouseWheelRotation = Input.mouseScrollDelta.y;
		if (_ShowRef == true && _BuildingRef != null) {
			_BuildingRef.transform.Rotate(Vector3.up, _MouseWheelRotation * _RotationSpeed);
		}
	}

	public void SpawnMummy()
	 {
		_SpawnMommy = true;
		_SpawnGhost = false;
		_SpawnGolem = false;
		_SpawnLighting = false;
		_MinionBuildingToSpawn = _MommyPrefab;
		SpawnMinionPrefab = true;
		_ShowRef = true;

		if (_BuildingRef == null)
		{
			AbleToBuild = true;
			_BuildingRef = Instantiate(_MinionBuildingToSpawn);
		} 
		else 
		{
			AbleToBuild = false;
			Destroy(_BuildingRef);
		}
	}

	public void SpawnGhost() 
	{
		_SpawnMommy = false;
		_SpawnGhost = true;
		_SpawnGolem = false;
		_SpawnLighting = false;
		_MinionBuildingToSpawn = _GhostPrefab;
		SpawnMinionPrefab = true;
		_ShowRef = true;

		if (_BuildingRef == null) 
		{
			AbleToBuild = true;
			_BuildingRef = Instantiate(_MinionBuildingToSpawn);
		}
		 else 
		 {
			 AbleToBuild = false;
			Destroy(_BuildingRef);
		}
	}

	public void SpawnGolem()
	 {
		_SpawnMommy = false;
		_SpawnGhost = false;
		_SpawnGolem = true;
		_SpawnLighting = false;
		_MinionBuildingToSpawn = _GolemPrefab;
		SpawnMinionPrefab = true;
		_ShowRef = true;

		if (_BuildingRef == null)
		{
			AbleToBuild = true;
			_BuildingRef = Instantiate(_MinionBuildingToSpawn);
		}
		else 
		{
			AbleToBuild = false;
			Destroy(_BuildingRef);
		}
	}

	public void SpawnLighting() 
	{
		_SpawnMommy = false;
		_SpawnGhost = false;
		_SpawnGolem = false;
		_SpawnLighting = true;
		SpawnMinionPrefab = true;
		_ShowRef = true;
		if (_BuildingRef == null) 
		{
			AbleToBuild = true;
			_BuildingRef = Instantiate(_LightingPrefab);
		} 
		else 
		{
			AbleToBuild = false;
			Destroy(_BuildingRef);
		}

	}

	private void DereferenceSpawnObj()
	{
		_SpawnMommy = false;
		_SpawnGhost = false;
		_SpawnGolem = false;
		_SpawnLighting = false;
		SpawnMinionPrefab = false;
		_ShowRef = false;
		AbleToBuild = false;
		Destroy(_BuildingRef);
		
	}

	# endregion ClassFunctions

	public float GetCDTimer()
	{
		return _SpawnCoolDown - _CoolDownTimer;
	}
}