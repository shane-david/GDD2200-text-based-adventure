#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SetFlag))]
public class SetFlagDrawer : PropertyDrawer
{   

    // overreiden from PropertyDrawer, called by the unity editor every frame to draw the insepctor 
    // window for each property, takes the position on the screen, the property to draw, and the label for that proprety
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        // start the property drawing 
        EditorGUI.BeginProperty(position, label, property);

        // get the child prperties for the SetFlag property so we can do things with them 
        var flagName = property.FindPropertyRelative("name");
        var flagChange = property.FindPropertyRelative("FlagChange");
        var changeAmount = property.FindPropertyRelative("ChangeAmount");

        // get the line height to draw the properties, add 2 for looks 
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;

        // create the rectangle that we will be drawing into within the inspector 
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        // draw flag name property
        EditorGUI.PropertyField(rect, flagName);

        // go down a line 
        rect.y += lineHeight;

        // drwa flag change property 
        EditorGUI.PropertyField(rect, flagChange);

        // get the value of the enum that flagChange currently is 
        var change = (FlagChanges)flagChange.enumValueIndex;

        // if flageChange is increase or decrese draw change amount, otherwise do not 
        if (change == FlagChanges.increase || change == FlagChanges.decrease)
        {
            rect.y += lineHeight;
            EditorGUI.PropertyField(rect, changeAmount);
        }

        // close the property 
        EditorGUI.EndProperty();
    }

    // overriden from Property drawer, unity calls this before OnGUI to get how much space for the properyt drawer
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {   

        // get the line heigh same as we did above
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;

        // get the flag chane and determine what enum it is 
        var flagChange = property.FindPropertyRelative("FlagChange");
        var change = (FlagChanges)flagChange.enumValueIndex;

        // if flageChange is incerase or decrease there are three lines, otherwise there are 2 
        int lines = (change == FlagChanges.increase || change == FlagChanges.decrease) ? 3 : 2;

        // return linehight * number of lines for how much should be drawn
        return lineHeight * lines;
    }
}

#endif