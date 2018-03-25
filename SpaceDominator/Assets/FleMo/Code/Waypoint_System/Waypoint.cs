using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour, IWaypoint
{
    [HideInInspector]
    public bool ShowInInspector = false;
    [SerializeField, Range(0, 100)]
    public float SpeedInPercent = 100;

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public float GetSpeed()
    {
        return SpeedInPercent;
    }
}
