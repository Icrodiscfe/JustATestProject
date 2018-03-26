using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingNode : IPathfindingNode
{
    private readonly Vector3 position;
    private bool isOccupied = false;

    public PathfindingNode(Vector3 position)
    {
        this.position = position;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }
}
