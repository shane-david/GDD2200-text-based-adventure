#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionFlag))]
public class ConditionFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var flagName = property.FindPropertyRelative("name");
        var flagCondition = property.FindPropertyRelative("FlagCondition");
        var compareAmount = property.FindPropertyRelative("Compare");

        float lineHeight = EditorGUIUtility.singleLineHeight + 2;
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(rect, flagName);
        rect.y += lineHeight;
        EditorGUI.PropertyField(rect, flagCondition);

        // Only show changeAmount if increase or decrease
        var change = (FlagConditions)flagCondition.enumValueIndex;
        if (change != FlagConditions.flagTrue && change != FlagConditions.flagFalse)
        {
            rect.y += lineHeight;
            EditorGUI.PropertyField(rect, compareAmount);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;
        var flagCondition = property.FindPropertyRelative("FlagCondition");
        var change = (FlagConditions)flagCondition.enumValueIndex;

        int lines = (change != FlagConditions.flagTrue && change != FlagConditions.flagFalse) ? 3 : 2;
        return lineHeight * lines;
    }
}
#endif