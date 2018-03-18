using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Motor Setting", menuName = "Scriptable Objects/Ship Motor Setting")]
public class ShipMotorSettings : ScriptableObject {

    [SerializeField]
    public float Thrust = 10000;
    [SerializeField]
    public float MaxSpeed = 100;
    [SerializeField]
    public float RotationAcceleration = 10000;
    [SerializeField]
    public float MaxRotationSpeed = 10000;
    [SerializeField]
    public float Brake = 10000;
}
