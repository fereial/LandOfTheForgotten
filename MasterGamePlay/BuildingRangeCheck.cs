using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRangeCheck : MonoBehaviour
{
    #region GlobalVariables

    [SerializeField]
    private float _PlayerDetectionRange = 0f;
    [SerializeField]
    private float _BuildingDetectionRange = 0f;
    [SerializeField]
    private GameObject _SpawnRef;
    [SerializeField]
    private int _Layer;
    [SerializeField]
    private Material _YesMat;
    [SerializeField]
    private Material _NoMat;
    private MeshRenderer _Mesh;



    #endregion GlobalVariables

    #region LifeCycle

    private void Awake()
    {
        _Mesh = this.transform.GetChild(0).GetComponent<MeshRenderer>();
        //  GetComponentInChildren<MeshRenderer>();
		_Layer = LayerMask.GetMask("IgnoreRaycast", "InvalidBuildingPlace");
    }

    private void Update()
    {
        SpawnInterference();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == _Layer)
        {
            _Mesh.material = _NoMat;
            MasterObjectSpawner.AbleToBuild = false;
        }
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.layer == _Layer)
        {
            _Mesh.material = _YesMat;
            MasterObjectSpawner.AbleToBuild = true;
        }
	}

    #endregion LifeCycle

    #region  ClassFunctions

    private void SpawnInterference()
    {

        foreach (var Players in PlayerNetworkManager.PlayerStats)
        {
            _SpawnRef.transform.localScale = new Vector3(_PlayerDetectionRange, 1, _PlayerDetectionRange);
            if (Vector3.Distance(transform.position, Players.transform.position) <= _PlayerDetectionRange)
            {
                _Mesh.material = _NoMat;
                MasterObjectSpawner.AbleToBuild = false;
            }
            else
            {
                _Mesh.material = _YesMat;
                MasterObjectSpawner.AbleToBuild = true;
            }
        }


        foreach (var Building in MasterObjectSpawner.BuildingInPlay)
        {

            if (Vector3.Distance(transform.position, Building.transform.position) <= _BuildingDetectionRange)
            {

                _Mesh.material = _NoMat;
                MasterObjectSpawner.AbleToBuild = false;

            }
            else
            {
                _Mesh.material = _YesMat;
                MasterObjectSpawner.AbleToBuild = true;
            }
        }

        
    }

    #endregion ClassFunctions
}
