using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    [SerializeField]
    public GameObject _SpawnableObject;
    [SerializeField, Range(0.1f, 100f)]
    public float _SpawnChance = 100;
}
