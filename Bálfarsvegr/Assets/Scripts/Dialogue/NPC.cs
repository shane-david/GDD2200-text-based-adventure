using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(fileName = "NPCName", menuName = "Dialogue/NPCs")]
public class NPC : ScriptableObject
{
    
    //-------------
    //private data
    //-------------
    [Header("NPC Data")]
    [SerializeField] private string Name; 
    [SerializeField] private GameObject NPCPrefab; 

    [Header("Node Information")]
    [SerializeField] private List<StartNodeOption> StartNodeOptions; // the node that the NPC should start on 
    private DialogueNode _currentNode;               // the current node the NPC is on 

    //-----------------------
    //Unity Lifetime Methods
    //-----------------------

    //TODO: error checking 
    private void OnEnable()
    {
         _currentNode = null; 
    }

    //---------------
    //private methods
    //---------------

    private void DetermineStartNode()
    {   

        // iterate throug the start node options and get a starting node 
        foreach (StartNodeOption node in StartNodeOptions)
        {   
            // assume the node status is true, it will be set to false if any flags are false
            bool nodeStatus = true; 

            // iterate through the flags of that option and check them
            foreach (ConditionFlag flag in node.Flags)
            {   
                // set the node status and break out if it is false
                nodeStatus = DetermineFlag(flag); 
                if (!nodeStatus) break; 
            }

            // if node status remaains true, make it the current node and break
            if (nodeStatus) {
                _currentNode = node.StartNode; 
                break; 
            }
        }

        // if after checking all the nodes there is still no current node, throw an erro
        if (_currentNode == null)
        {
            throw new System.Exception($"[NPC {Name}] has no valid start nodes!"); 
        }
    }

    private bool DetermineFlag(ConditionFlag flag)
    {   

        // if it is a quest flag do the quest method instead of the determine flag method 
        if (flag.name.Substring(0,5) == "quest")
        {   

            // if they want it to be true return if it is active 
            if (flag.FlagCondition == FlagConditions.flagTrue) {

                return FlagManager.Instance.IsQuestActive(flag.name); 

            // otherwise if it needs to be false, return if it is not active 
            } else if (flag.FlagCondition == FlagConditions.flagFalse) {

                return !FlagManager.Instance.IsQuestActive(flag.name); 
                
            }
        }

        // if it is a boolean flag the condtion will be true or false, so determine the value
        if (flag.FlagCondition == FlagConditions.flagTrue || flag.FlagCondition == FlagConditions.flagFalse) {
            
            return FlagManager.Instance.DetermineFlag(flag.name, flag.FlagCondition); 

        // otherwise it is a number flag
        } else {
            
            return FlagManager.Instance.DetermineFlag(flag.name, flag.FlagCondition, flag.Compare); 

        }
    }

    //--------------
    //public methods
    //--------------


    // determine the NPC's start node and begin the rendering 
    public void StartDialogue()
    {
        DetermineStartNode(); 
    }

    //-------
    //getters
    //-------

    // get the current node's dialogue, this is called by the 
    // dialouge scene manager to render it onto the TMP component
    public string GetDialogue()
    {
        
        // if it is a player thought return just the dialaouge
        if (_currentNode.IsPlayerThought())  {

            return _currentNode.GetDialogue(); 

        // otherwise append the NPC's name
        } else {
            return Name + ": " + _currentNode.GetDialogue(); 
        }
    }

    public List<string> GetChoiceStrings()
    {
        
        // instantiate the list 
        List<string> returnStrings = new(); 

        // iterate through the current nodes choices 
        foreach (var choice in _currentNode.GetChoices())
        {
            
            // set a boolean to say the choice status is renderabel
            bool choiceStatus = true; 

            // iterate through each choice's needed flags 
            foreach (var flag in choice.NeededFlags)
            {
                choiceStatus = DetermineFlag(flag); 
            }

            // if the status remains true, add it to the list of return string
            if (choiceStatus)
            {
                returnStrings.Add(choice.GetText()); 
            }
        }

        return returnStrings; 
    }

    public PlayerChoice GetChoice(int index)
    {
        return _currentNode.GetChoices()[index]; 
    }

    public string GetName() => Name;

    public GameObject GetPrefab() => NPCPrefab; 

    //--------
    //setters
    //--------

    public void SetCurrentNode(DialogueNode node)
    {
        _currentNode = node; 
    }

}