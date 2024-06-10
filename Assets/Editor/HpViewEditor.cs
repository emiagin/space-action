using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(HpView))]
public class HpViewEditor : Editor
{
	private ReorderableList list;

	private void OnEnable()
	{
		list = new ReorderableList(serializedObject,
				serializedObject.FindProperty("healthSpriteLevels"),
				true, true, true, true);

		list.drawHeaderCallback = (Rect rect) =>
		{
			EditorGUI.LabelField(rect, "Health Sprite Levels");
		};

		list.drawElementCallback =
			(Rect rect, int index, bool isActive, bool isFocused) =>
			{
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				rect.y += 2;
				float spriteWidth = rect.width * 0.7f;
				float levelWidth = rect.width * 0.3f;

				EditorGUI.PropertyField(
					new Rect(rect.x, rect.y, spriteWidth, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative("sprite"), GUIContent.none);
				EditorGUI.PropertyField(
					new Rect(rect.x + spriteWidth, rect.y, levelWidth, EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative("level"), GUIContent.none);
			};
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}
