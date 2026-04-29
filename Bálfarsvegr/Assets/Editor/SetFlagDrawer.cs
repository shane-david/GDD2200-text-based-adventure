#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SetFlag))]
public class SetFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var flagName = property.FindPropertyRelative("name");
        var flagChange = property.FindPropertyRelative("FlagChange");
        var changeAmount = property.FindPropertyRelative("ChangeAmount");

        float lineHeight = EditorGUIUtility.singleLineHeight + 2;
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(rect, flagName);
        rect.y += lineHeight;
        EditorGUI.PropertyField(rect, flagChange);

        // Only show changeAmount if increase or decrease
        var change = (FlagChanges)flagChange.enumValueIndex;
        if (change == FlagChanges.increase || change == FlagChanges.decrease)
        {
            rect.y += lineHeight;
            EditorGUI.PropertyField(rect, changeAmount);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;
        var flagChange = property.FindPropertyRelative("FlagChange");
        var change = (FlagChanges)flagChange.enumValueIndex;

        int lines = (change == FlagChanges.increase || change == FlagChanges.decrease) ? 3 : 2;
        return lineHeight * lines;
    }
}
#endif