using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerChoice
{

    [HideInInspector] public string name; 

    //-------------
    //private data
    //-------------

    [Header("UI")]
    [TextArea(1,10)]
    [SerializeField] private string       ChoiceText;  // the text to be displayed on the choice 

    [Header("Flags")]

    [SerializeField] private List<ConditionFlag> NeededFlags; // flags necessary for this option to appear
    [SerializeField] private List<SetFlag> FlagsToSet;  // flags that will be set if this option is selected

    [Header("Flow")]
    [SerializeField] private DialogueNode NextNode;    // the node that the player will be directed to once this choie is selected
    public bool isLast; // whether or not this is the last node in the conversatoin 
    
    //---------------
    //public methods
    //---------------
    
    //TODO: handle the logic for when this choice is selected
    public void Selected()
    {
        
    }

    //TODO: try to go to the next node
    public bool TryGoNext()
    {
        return true; 
    }

    //--------
    //getters
    //--------
    
    // try to get the text assuming it exists
    public string GetText()
    {
        if (String.IsNullOrEmpty(ChoiceText))
        {
            Debug.LogWarning("[PlayerChoices] invalid text!"); 
            return "ERROR"; 
        }

        return ChoiceText; 
    }

    //-------
    //setters
    //-------

    #if UNITY_EDITOR
    // set name for inspector (editor only)
    public void SetName()
    {
        name = ChoiceText; 
    }
    #endif 
    


}
