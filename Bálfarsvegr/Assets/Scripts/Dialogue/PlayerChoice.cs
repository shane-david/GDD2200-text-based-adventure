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

    public List<ConditionFlag> NeededFlags; // flags necessary for this option to appear
    [SerializeField] private List<SetFlag> FlagsToSet;  // flags that will be set if this option is selected

    [Header("Flow")]
    [SerializeField] private DialogueNode NextNode;    // the node that the player will be directed to once this choie is selected
    public bool isLast; // whether or not this is the last node in the conversatoin 
    
    //---------------
    //public methods
    //---------------
    
    //try to go to the next node, if you can set the next node of the NPC and return true
    //if you can not return false and the caller will handle the logic
    public bool TryGoNext(NPC npc)
    {   
        // if its not last set the new current node to next node
        if (!isLast)
        {   
            npc.SetCurrentNode(NextNode); 
            return true; 
        }

        return false; 
    }

    // sets the flags for when that choice is sleected
    public void SetFlags()
    {
        
        // iterate through the set flags list and call it from the flag maanager
        foreach (var flag in FlagsToSet)
        {   

            // if the flag is a quest, we want to set it as so 
            if (flag.name.Substring(0,5) == "quest")
            {   

                // if we are setting the quest as true add it 
                if (flag.FlagChange == FlagChanges.setTrue) {

                    FlagManager.Instance.StartQuest(flag.name); 

                // if we are setting the flag as false, remove it 
                } else if (flag.FlagChange == FlagChanges.setFalse) {
                    
                    FlagManager.Instance.CompleteQuest(flag.name); 

                // otherwise throw an error
                } else {
                    throw new System.Exception($"[PlayerChoice] {flag.name} is not being set to true or false!"); 
                }

                return; 
            }

            // if the flag is a scene set, we want to set it as so 
            if (flag.name.Substring(0,5) == "scene")
            {
                GameManager.Instance.NextScene = flag.name.Substring(5); 
                return; 
            }

            // if it is a number flag it will be increase or decrese
            if (flag.FlagChange == FlagChanges.increase) {
                FlagManager.Instance.SetFlag(flag.name, flag.ChangeAmount);
            } else if (flag.FlagChange == FlagChanges.decrease) {
                FlagManager.Instance.SetFlag(flag.name, -1 * flag.ChangeAmount);

            // otherwise it is set true or set false
            } else {
                FlagManager.Instance.SetFlag(flag.name, flag.FlagChange == FlagChanges.setTrue); 
            }
        }
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
