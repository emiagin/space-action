using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathfindingGrid))]
public class PathfindingGridEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PathfindingGrid pathfindingGrid = (PathfindingGrid)target;
		if (GUILayout.Button("Update Grid"))
		{
			pathfindingGrid.CreateGrid();
			EditorUtility.SetDirty(target);
		}
	}
}

