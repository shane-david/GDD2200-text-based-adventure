using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewNode", menuName = "Dialogue/Nodes")]
public class DialogueNode : ScriptableObject
{

    //-------------
    //private data
    //-------------

    [Header("NPC")]
    [TextArea(3,10)]
    [SerializeField] private string NPCText;                   // the text the NPC will be displaying

    [Header("Player")]
    [SerializeField] private List<PlayerChoice> PlayerChoices; // the choices the player has for this NPC text 

    //---------------
    //public methods
    //---------------

    //TODO: functionality to render the dialogue for the node
    public void RenderDialogue()
    {
        
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

