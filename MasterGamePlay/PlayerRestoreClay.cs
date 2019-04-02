using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestoreClay : MonoBehaviour
{
    [SerializeField]
    private FloatVar _CurrentClayPool;

    [SerializeField]
    private float _RegenClay = 100f;

    private Health _PLayerHealth;

    private void OnEnable()
    {
        _PLayerHealth.OnHealthChange += OnHealthChange;
        ResourcePoolEvents.RechargePoolEvent += RestoreClayPool;

    }

    private void OnDisable()
    {
        _PLayerHealth.OnHealthChange -= OnHealthChange;
        ResourcePoolEvents.RechargePoolEvent -= RestoreClayPool;
    }

    private void Awake()
    {
        _PLayerHealth = GetComponent<Health>();
    }


    public void RestoreClayPool(float Clay)
    {

        _CurrentClayPool.Value += Clay;
        if (_CurrentClayPool.Value >= MasterResourceController.InitialClayPool)
        {
            _CurrentClayPool.Value = MasterResourceController.InitialClayPool;
            return;
        }
    }

    private void OnHealthChange(float health, bool isHealing)
    {
        if (health <= 0)
        {

            RestoreClayPool(_RegenClay);
        }
    }
}
