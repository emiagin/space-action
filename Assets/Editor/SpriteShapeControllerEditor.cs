using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

[CustomEditor(typeof(SpriteShapeController))]
public class SpriteShapeControllerEditor : Editor
{
	void OnSceneGUI()
	{
		SpriteShapeController controller = (SpriteShapeController)target;
		Spline spline = controller.spline;

		Handles.color = Color.yellow;

		for (int i = 0; i < spline.GetPointCount(); i++)
		{
			Vector3 pointPosition = controller.transform.TransformPoint(spline.GetPosition(i)); // Get the world position of the spline point
			Vector3 labelPosition = pointPosition + Vector3.up * 0.2f; // Offset the label position slightly above the point
			Handles.Label(labelPosition, $"({pointPosition.x:F2}, {pointPosition.y:F2}) {i}");
		}
	}
}
