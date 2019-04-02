using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterClayPicker : MonoBehaviour
{

    [SerializeField]
    private string _LayerName;

    private int _LayerValue;
    private Camera _MyCamera;
    private bool _PickUp = true;

    private void Awake()
    {
        _MyCamera = GetComponentInChildren<Camera>();
        _LayerValue = LayerMask.GetMask(_LayerName);
      
    }

    private void Update()
    {
         PickupItem();
    }
    

    private void PickupItem()
    {
        Ray ray = _MyCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit RayHit;

        if (Physics.Raycast(ray, out RayHit, 1000, _LayerValue))
        {
            var Pickable = RayHit.transform.GetComponent<IPoolRechargeable>();
            Pickable.RechargePool();
        }

    }
    
}
