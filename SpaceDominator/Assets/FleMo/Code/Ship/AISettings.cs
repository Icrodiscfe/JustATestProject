using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI Setting", menuName = "Scriptable Objects/AI Setting")]
public class AISettings : ScriptableObject
{
    [SerializeField]
    public float Sight = 70;
    [SerializeField]
    public float MinApproachDistance = 20;
    [SerializeField]
    private float MaxApproachDistance = 30;
}
