using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnableObject[] _SpawnableObjects;
    [SerializeField]
    private float _MinSpawnDistance = 100;
    [SerializeField]
    private float _MaxSpawnDistance = 500;

    private float spawnSum;
    private GameObject player;

    void Start()
    {
        foreach(SpawnableObject so in _SpawnableObjects)
        {
            if(so == null)
            {
                Debug.LogError("Empty SpawnableObject slot.");
                continue;
            }

            spawnSum += so._SpawnChance;
        }

        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    /// <summary>
    /// Spawns a random object near players position
    /// </summary>
    public void Spawn()
    {
        SpawnableObject spawnableObject = GetRandomSpawnableObject();

        GameObject go = Instantiate(spawnableObject._SpawnableObject);
        ISpawnableObject iso = go.GetComponent<ISpawnableObject>();
        float distance = Random.Range(_MinSpawnDistance, _MaxSpawnDistance);

        iso.Spawn(player.transform.position + MyRandom.RandomVector3InCircle(distance));
    }

    /// <summary>
    /// Returns a random spawnable object
    /// </summary>
    /// <returns>Random Spawnable Object</returns>
    private SpawnableObject GetRandomSpawnableObject()
    {
        float chance = Random.Range(0, spawnSum);
        float counter = 0;

        foreach(SpawnableObject spawnableObject in _SpawnableObjects)
        {
            counter += spawnableObject._SpawnChance;
            if(chance <= counter)
            {
                return spawnableObject;
            }
        }

        Debug.LogError("Something wrent wrong with chance-calculation on spawnable object");
        return default(SpawnableObject);
    }

    #region On Validate
    void OnValidate()
    {
        ValidateSpawnableObject();
        ValidateSpawnDistance();
    }

    void ValidateSpawnableObject()
    {
        SpawnableObject[] spawnableObjects = new SpawnableObject[_SpawnableObjects.Length];

        for (int i = 0; i < _SpawnableObjects.Length; i++)
        {
            var item = _SpawnableObjects[i];
            if (item == null)
            {
                spawnableObjects[i] = item;
                continue;
            }

            ISpawnableObject iso = item._SpawnableObject.GetComponent<ISpawnableObject>();

            if (iso == null)
            {
                spawnableObjects[i] = null;
            }
            else
            {
                spawnableObjects[i] = item;
            }
        }

        _SpawnableObjects = spawnableObjects;
    }

    void ValidateSpawnDistance()
    {
        if(_MaxSpawnDistance < _MinSpawnDistance)
        {
            _MaxSpawnDistance = _MinSpawnDistance;
        }
    }
    #endregion
}