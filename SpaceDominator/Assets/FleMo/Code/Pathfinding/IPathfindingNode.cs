using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfindingNode
{
    Vector3 GetPosition();
    bool IsOccupied();
    void SetOccupied(bool occupied);
}
