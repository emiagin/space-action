using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HealthSpriteLevel))]
public class HealthSpriteLevelDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		var spriteProperty = property.FindPropertyRelative("sprite");
		var levelProperty = property.FindPropertyRelative("level");

		float spriteWidth = position.width * 0.7f;
		float levelWidth = position.width * 0.3f;

		Rect spriteRect = new Rect(position.x, position.y, spriteWidth, position.height);
		Rect levelRect = new Rect(position.x + spriteWidth, position.y, levelWidth, position.height);

		EditorGUI.PropertyField(spriteRect, spriteProperty, GUIContent.none);
		EditorGUI.PropertyField(levelRect, levelProperty, GUIContent.none);

		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight;
	}
}
