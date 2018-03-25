using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointGroup)), CanEditMultipleObjects]
public class WaypointGroupEditor : Editor
{
    private Waypoint selectedWaypoint = null;

    public override void OnInspectorGUI()
    {
        WaypointGroup wpg = (WaypointGroup)target;

        if(wpg.Waypoints == null)
        {
            wpg.Waypoints = new List<GameObject>();
        }

        EditorGUILayout.HelpBox("Add empty gameobjects and order them for waypoints." +
            " After added press build. On changes press build again. First waypoint will be set to goup position.", MessageType.None);

        if(GUILayout.Button("Add waypoint"))
        {
            GameObject wp = new GameObject();
            wp.name = "Waypoint " + (wpg.Waypoints.Count + 1);
            wp.AddComponent(typeof(Waypoint));
            wp.transform.position = wpg.transform.position;

            wp.transform.SetParent(wpg.gameObject.transform);
            wpg.Waypoints.Add(wp);
        }

        if(GUILayout.Button("Clear waypoints"))
        {
            foreach(GameObject wp in wpg.Waypoints)
            {
                DestroyImmediate(wp);
            }

            wpg.Waypoints.Clear();
        }


        for(int i = 0; i < wpg.Waypoints.Count; i++)
        {
            string name = "Waypoint " + (i + 1);
            EditorGUILayout.BeginHorizontal();
            Waypoint wp = wpg.Waypoints[i].GetComponent<Waypoint>();

            if (GUILayout.Button(name, EditorStyles.miniButton))
            {
                Tools.current = Tool.None;
                selectedWaypoint = wpg.Waypoints[i].GetComponent<Waypoint>();
            }

            if(GUILayout.Button("Up", EditorStyles.miniButton))
            {

            }

            if(GUILayout.Button("Down", EditorStyles.miniButton))
            {

            }

            if(GUILayout.Button("Delete", EditorStyles.miniButton))
            {
                GameObject wpGo = wpg.Waypoints[i];
                wpg.Waypoints.Remove(wpGo);
                DestroyImmediate(wpGo);
            }

            EditorGUILayout.EndHorizontal();

            wp.ShowInInspector = EditorGUILayout.Foldout(wp.ShowInInspector, name);

            if (wp.ShowInInspector)
            {
                CreateEditor(wpg.Waypoints[i].GetComponent<Waypoint>()).DrawDefaultInspector();
            }
        }
    }

    void OnSceneGUI()
    {
        if(selectedWaypoint == null)
        {
            return;
        }

        Tools.current = Tool.None;
        EditorGUI.BeginChangeCheck();
        Vector3 ntp = Handles.PositionHandle(selectedWaypoint.transform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selectedWaypoint, "Move waypoint");
            selectedWaypoint.transform.position = ntp;
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    private static void DrawGizmo(WaypointGroup scr, GizmoType type)
    {
        WaypointGroup wpg = (WaypointGroup)scr;

        if (wpg.Waypoints == null || wpg.Waypoints.Count == 0)
        {
            return;
        }

        Vector3? last = null;

        foreach (GameObject go in wpg.Waypoints)
        {
            Waypoint wp = go.GetComponent<Waypoint>();

            if (last.HasValue)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(last.Value, wp.transform.position);
            }

            last = wp.transform.position;
        }

        Gizmos.DrawLine(last.Value, wpg.Waypoints[0].GetComponent<Waypoint>().transform.position);
    }


}
