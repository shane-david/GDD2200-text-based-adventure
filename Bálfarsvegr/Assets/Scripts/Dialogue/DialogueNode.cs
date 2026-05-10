using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewNode", menuName = "Dialogue/Nodes")]
public class DialogueNode : ScriptableObject
{

    //-------------
    //private data
    //-------------

    [Header("Text")]
    [TextArea(3,10)]
    [SerializeField] private string DisplayText;                   // the text the NPC will be displaying
    [SerializeField] private bool   isPlayerThought; // whether or not to draw the text as a player thought or a npc line

    [Header("Choices")]
    [SerializeField] private List<PlayerChoice> PlayerChoices; // the choices the player has for this NPC text 

    //--------
    //getters
    //--------
    public string GetDialogue() => DisplayText; 

    public bool IsPlayerThought() => isPlayerThought; 

    public List<PlayerChoice> GetChoices()
    {
        // instantiate the list 
        List<PlayerChoice> validChoices = new(); 

        // iterate through the current nodes choices 
        foreach (var choice in PlayerChoices)
        {
            
            // set a boolean to say the choice status is renderabel
            bool choiceStatus = true; 

            // iterate through each choice's needed flags 
            foreach (var flag in choice.NeededFlags)
            {
                // if it is a boolean flag the condtion will be true or false, so determine the value
                if (flag.FlagCondition == FlagConditions.flagTrue || flag.FlagCondition == FlagConditions.flagFalse) {
                    
                    choiceStatus = FlagManager.Instance.DetermineFlag(flag.name, flag.FlagCondition); 

                // otherwise it is a number flag
                } else {
                    
                    choiceStatus = FlagManager.Instance.DetermineFlag(flag.name, flag.FlagCondition, flag.Compare); 

                }
            }

            // if the status remains true, add it to the list of return string
            if (choiceStatus)
            {
                validChoices.Add(choice); 
            }
        }

        // return only the valid choices 
        return validChoices; 
    }

    //------------
    //EDITOR ONLY
    //------------
    #if UNITY_EDITOR

    // set the name of the choices on validate, so it looks better in inpsecotr
    private void OnValidate()
    {
        foreach (var choice in PlayerChoices)
        {
            choice.SetName(); 
        }
    }
    #endif 
}

