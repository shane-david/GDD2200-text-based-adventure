#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionFlag))]
public class ConditionFlagDrawer : PropertyDrawer
{

    // overreiden from PropertyDrawer, called by the unity editor every frame to draw the insepctor 
    // window for each property, takes the position on the screen, the property to draw, and the label for that proprety
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        // start the property drawing 
        EditorGUI.BeginProperty(position, label, property);

        // get the child properteis of the COnditionFlag property so we can do thigns with them
        var flagName = property.FindPropertyRelative("name");
        var flagCondition = property.FindPropertyRelative("FlagCondition");
        var compareAmount = property.FindPropertyRelative("Compare");

        // get the line height to draw the properties, add 2 for looks 
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;

        // create the rectangle that we will be drawing into within the inspector 
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        // draw flag name property
        EditorGUI.PropertyField(rect, flagName);

        // go to the next line 
        rect.y += lineHeight;

        // draw flag gonition property
        EditorGUI.PropertyField(rect, flagCondition);

        // get the condition as its enum value 
        var condition = (FlagConditions)flagCondition.enumValueIndex;

        // if flagCondition is not true nad not false, we can draw the compareAmount 
        if (condition != FlagConditions.flagTrue && condition != FlagConditions.flagFalse)
        {
            rect.y += lineHeight;
            EditorGUI.PropertyField(rect, compareAmount);
        }

        // close the property
        EditorGUI.EndProperty();
    }

    // overriden from Property drawer, unity calls this before OnGUI to get how much space for the properyt drawer
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {   

        // get the line height same as we did above
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;

        // get the flag conditoin property and its enum value
        var flagCondition = property.FindPropertyRelative("FlagCondition");
        var condition = (FlagConditions)flagCondition.enumValueIndex;

        // if the condition is not true and not false, there are three lines, otherwise there are two
        int lines = (condition != FlagConditions.flagTrue && condition != FlagConditions.flagFalse) ? 3 : 2;

        // return the line height * number of lines to get size of property 
        return lineHeight * lines;
    }
}
#endif