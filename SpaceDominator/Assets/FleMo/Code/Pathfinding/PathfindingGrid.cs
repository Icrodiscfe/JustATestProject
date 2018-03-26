//http://www.jgallant.com/nodal-pathfinding-in-unity-2d-with-a-in-non-grid-based-games/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{
    [SerializeField]
    private float _Distance = 1f;
    [SerializeField]
    private float _RowOffset = 0.5f;
    [SerializeField]
    private float _Height = 0;

    [Header("Visualization")]
    [SerializeField]
    private Vector3 _Size = new Vector3(1, 1, 1);
    [SerializeField]
    private Color _IsFreeColor = Color.green;
    [SerializeField]
    private Color _IsOccupiedColor = Color.red;

    List<IPathfindingNode> nodes = new List<IPathfindingNode>();
    private bool visualize = false;

    void Start ()
    {
        nodes = new List<IPathfindingNode>();
        GenerateGrid(new Vector3(0, _Height, 0), 20, 20);
	}

    /// <summary>
    /// Generates a grid and returns it, but will not saved anywhere. Should only be used for visualization.
    /// </summary>
    /// <returns>Array of IPathFindingNodes</returns>
    [ExecuteInEditMode]
    public void Visualize()
    {
        if (nodes.Count <= 0)
        {
            nodes = GenerateGrid(Vector3.zero, 20, 20);
        }

        visualize = true;
    }

    [ExecuteInEditMode]
    public void ClearVisualization()
    {
        nodes.Clear();
        visualize = false;
    }

    [ExecuteInEditMode]
    private List<IPathfindingNode> GenerateGrid(Vector3 start, int columns, int rows)
    {
        List<IPathfindingNode> list = new List<IPathfindingNode>();

        Vector3 pos = start;
        for(int z = 0; z < rows; z++)
        {
            for(int x = 0; x < columns; x++)
            {
                pos.x = _Distance * x;

                if(z % 2 > 0)
                {
                    pos.x += _RowOffset;
                }

                list.Add(new PathfindingNode(pos));
            }

            pos.z += _Distance;
        }

        return list;
    }

    void OnDrawGizmos()
    {
        if(nodes.Count <= 0)
        {
            return;
        }

        foreach(IPathfindingNode pathfindingNode in nodes)
        {
            if (pathfindingNode.IsOccupied())
            {
                Gizmos.color = _IsOccupiedColor;
            }
            else
            {
                Gizmos.color = _IsFreeColor;
            }
            Gizmos.DrawCube(pathfindingNode.GetPosition(), _Size);
        }
    }
}
