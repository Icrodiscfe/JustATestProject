using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIGroup : MonoBehaviour, IAIGroup, ISpawnableObject {

    GameObject player;
    IAIShip[] iAIShips;

    [Header("Spawn")]
    [SerializeField]
    private SpawnableShip[] _SpawnableShips;
    [SerializeField, Range(20, 100)]
    private float _RadiusMin = 20;
    [SerializeField, Range(20, 100)]
    private float _RadiusMax = 100;
    [SerializeField, Range(1, 20)]
    private int _UnitsMin = 1;
    [SerializeField, Range(1, 20)]
    private int _UnitsMax = 20;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        iAIShips = transform.Cast<Transform>().Select(x => x.GetComponent<IAIShip>()).ToArray();
    }

    void Update()
    {
        bool isInRange = false;
        foreach(IAIShip ship in iAIShips)
        {
            isInRange = CheckIfPlayerInRange();
            
            if(isInRange)
            {
                break;
            }
        }

        if(isInRange)
        {
            SetTarget(player.transform.position);
        }
        else
        {
            SetTarget(null);
        }
    }

    public void SetTarget(Vector3? position)
    {
        foreach(IAIShip child in iAIShips)
        {
            child.SetTarget(position);
        }
    }

    private bool CheckIfPlayerInRange()
    {
        foreach (Transform shipTransform in transform)
        {
            IAIShip ship = shipTransform.GetComponent<IAIShip>();
            float distance = Vector3.Distance(shipTransform.position, player.transform.position);

            if (distance <= ship.GetAISettings().Sight && distance > ship.GetAISettings().MinApproachDistance)
            {
                return true;
            }
        }

        return false;
    }

    public void Spawn(Vector3 position)
    {
        var x = 2;
    }

    #region On validate
    void OnValidate()
    {
        ValidateSpawnableShips();
        ValidateRanges();
        ValidateUnitsCount();
    }

    void ValidateSpawnableShips()
    {
        foreach(var item in _SpawnableShips)
        {
            if(item == null)
            {
                continue;
            }

            item.OnValidate();
        }
    }

    void ValidateRanges()
    {
        if(_RadiusMin > _RadiusMax)
        {
            _RadiusMax = _RadiusMin;
        }
    }

    void ValidateUnitsCount()
    {
        if(_UnitsMin > _UnitsMax)
        {
            _UnitsMax = _UnitsMin;
        }
    }
    #endregion
}
