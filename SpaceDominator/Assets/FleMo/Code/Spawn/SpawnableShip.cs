using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnableShip {

    [SerializeField]
    public UnityEngine.GameObject _SpawnableShip;
    [SerializeField, Range(0.1f, 100)]
    public float _SpawnChance = 100f;

    public void OnValidate()
    {
        ValidateSpawnableShips();
    }

    void ValidateSpawnableShips()
    {
        IAIShip iAIShip = _SpawnableShip.GetComponent<IAIShip>();

        if (iAIShip == null)
        {
            _SpawnableShip = null;
        }
    }
}
