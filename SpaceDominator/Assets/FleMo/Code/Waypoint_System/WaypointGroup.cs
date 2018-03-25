using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGroup : MonoBehaviour, IWaypointGroup
{
    [SerializeField]
    public List<GameObject> Waypoints = new List<GameObject>();

    private IWaypoint[] waypoints;
    public IWaypoint[] GetWaypoints()
    {
        if(waypoints == null)
        {
            waypoints = new IWaypoint[Waypoints.Count];
            for(int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = Waypoints[i].GetComponent<IWaypoint>();
            }
        }
        
        return waypoints;
    }
}
