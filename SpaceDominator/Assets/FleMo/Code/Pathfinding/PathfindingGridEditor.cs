using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathfindingGrid)), CanEditMultipleObjects]
public class PathfindingGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PathfindingGrid pg = (PathfindingGrid)target;

        EditorGUILayout.HelpBox("Edit settings an press Build to visualize your settings.", MessageType.Info);
        DrawDefaultInspector();

        if (GUILayout.Button("Visualize"))
        {
            pg.Visualize();
        }

        if(GUILayout.Button("Clear"))
        {
            pg.ClearVisualization();
        }
    }
}
