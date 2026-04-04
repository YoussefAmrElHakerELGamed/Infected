using System;
using UnityEngine;

public class PlayerHealthProgressPar : MonoBehaviour
{
    private int _numberOfHealthIndicators;
    private Transform _t;
    void Start()
    {
        _t = transform;
        _numberOfHealthIndicators = _t.childCount;
        GameEventBus.Instance.OnPlayerTakeDamage += CalculateAndDisplayHealthLevel;
    }

    private void CalculateAndDisplayHealthLevel(PlayerTakeDamageEventArgs args)
    {
        int _numberOfHealthIndicatorsForCurrentHealth = Mathf.RoundToInt(args.Health % (args.MaxHealth / _numberOfHealthIndicators));
        int _indicatorsToDestroy = _numberOfHealthIndicators - _numberOfHealthIndicatorsForCurrentHealth;

        for (int _idx = 0; _idx < _indicatorsToDestroy; _idx++)
        {
            Destroy(_t.GetChild(_idx).gameObject);
        }
    }
}
