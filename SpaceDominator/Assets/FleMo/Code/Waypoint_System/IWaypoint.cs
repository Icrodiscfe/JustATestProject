using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaypoint
{
    /// <summary>
    /// Get the position of the waypoint
    /// </summary>
    /// <returns>Returns the speed of the waypoint in percentage</returns>
    Vector3 GetPosition();
    float GetSpeed();
}
