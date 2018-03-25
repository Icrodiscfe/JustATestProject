using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypointGroup
{
    /// <summary>
    /// Get the waypoints in the group
    /// </summary>
    /// <returns>Array of IWaypoints</returns>
    IWaypoint[] GetWaypoints();
}
