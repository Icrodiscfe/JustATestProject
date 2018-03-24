using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIGroup : MonoBehaviour, ISpawnableObject {

    GameObject player;
    Rigidbody playerRigidbody;
    IAIShip[] iAIShips;
    float spawnSum = 0;

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
        playerRigidbody = player.GetComponent<Rigidbody>();
        iAIShips = transform.Cast<Transform>().Select(x => x.GetComponent<IAIShip>()).ToArray();

        foreach(SpawnableShip ss in _SpawnableShips)
        {
            spawnSum += ss._SpawnChance;
        }
    }

    void Update()
    {
        if(CheckIfPlayerInRange())
        {
            SetTargetByRigidbody(playerRigidbody);
        }
        else
        {
            SetTargetByRigidbody(null);
        }
    }

    /// <summary>
    /// Set the target where the ai ships should fly to
    /// </summary>
    /// <param name="position">Null if stay at position</param>
    private void SetTargetByVector(Vector3? position)
    {
        foreach(IAIShip child in iAIShips)
        {
            child.SetTarget(position);
        }
    }

    private void SetTargetByRigidbody(Rigidbody rigidbody)
    {
        foreach (IAIShip child in iAIShips)
        {
            child.SetTarget(rigidbody);
        }
    }

    /// <summary>
    /// Check if player is in range of any of the ai in the group
    /// </summary>
    /// <returns>True if player is in range</returns>
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

    #region spawn
    /// <summary>
    /// Spawns random group of enemys
    /// </summary>
    /// <param name="position">Position of Group</param>
    public void Spawn(Vector3 position)
    {
        this.transform.position = position;
        int unitsToSpawn = UnityEngine.Random.Range(_UnitsMin, _UnitsMax);
        iAIShips = new IAIShip[unitsToSpawn];

        for(var i = 0; i < unitsToSpawn; i++)
        {
            GameObject ship = Instantiate(GetRandomSpawnableShip());
            iAIShips[i] = ship.GetComponent<IAIShip>();
            ship.transform.SetParent(transform);
            ship.transform.position = MyRandom.RandomVector3InCircle(_RadiusMin, _RadiusMax) + this.transform.position;
        }
    }

    /// <summary>
    /// Returns a random spawnable ship
    /// </summary>
    /// <returns>Spawnable ship as GameObject</returns>
    private GameObject GetRandomSpawnableShip()
    {
        float chance = UnityEngine.Random.Range(0, spawnSum);
        float counter = 0;

        foreach(SpawnableShip ss in _SpawnableShips)
        {
            counter += ss._SpawnChance;

            if(chance < counter)
            {
                return ss._SpawnableShip;
            }
        }

        Debug.LogError("Something wrent wrong with chance-calculation on spawnable ship");
        return default(GameObject);
    }
    #endregion

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
