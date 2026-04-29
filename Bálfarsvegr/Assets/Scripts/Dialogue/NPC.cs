using System.Collections.Generic;
using UnityEngine; 

[CreateAssetMenu(fileName = "NPCName", menuName = "Dialogue/NPCs")]
public class NPC : ScriptableObject
{
    
    //-------------
    //private data
    //-------------
    [Header("NPC Data")]
    [SerializeField] private string Name; 

    [Header("Node Information")]
    [SerializeField] private List<StartNodeOption> StartNodeOptions; // the node that the NPC should start on 
    private DialogueNode _currentNode;               // the current node the NPC is on 

    //-----------------------
    //Unity Lifetime Methods
    //-----------------------

    //TODO: error checking and configure current node for start
    private void Awake()
    {
         
    }

    //--------------
    //public methods
    //--------------

    // TODO: enanable this NPC in the scene
    public void StartDialogue()
    {
        
    }

    // TODO: render the current npc and their dialogue node
    public void RenderNPC()
    {
        
    }

}