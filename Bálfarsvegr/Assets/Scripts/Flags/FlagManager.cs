using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagManager : Singleton<FlagManager>
{
    

    //-------------
    //private data
    //-------------
    
    private Dictionary<string, bool> _booleanFlags = new();
    private Dictionary<string, int>  _numberFlags  = new();

    private QuestManager _questManager = new(); 

    //----------------------
    //Unity Lifetime Methods
    //----------------------

    // configure starting flags
    protected override void Awake()
    {   
        base.Awake(); 

        // NPC talked to flags
        SetFlag("hasTalkedToHalfdan", false); 
        SetFlag("hasTalkedToDagr", false); 

        // player progress flags
        SetFlag("powerConquest", false); 

        // Player stats
        SetFlag("humanityScore", 0); 
        SetFlag("shipDamage", 0); 

        // player inventory
        SetFlag("loftjarnCount", 0); 
        SetFlag("breathCount", 0); 
        SetFlag("potionCount", 0); 
    }

    //--------------
    //public methods
    //--------------

    // set flag method for the number flags
    public void SetFlag(string flag, int change)
    {   
        // if it does exist add it
        bool doesNotExist = _numberFlags.TryAdd(flag, change); 

        // if it does not exist, just update the value
        if (!doesNotExist) {
            _numberFlags.TryGetValue(flag, out int amount);
            _numberFlags[flag] = amount + change; 
        } 
    }

    // set flage override for the boolean flags
    public void SetFlag(string flag, bool value)
    {
        _booleanFlags[flag] = value; 
    }

    // returns a boolean for weather the flag is part of what you specified
    // this is specifically for ints because otherwise it can just return the result of the flage
    public bool DetermineFlag(string flag, FlagConditions condition, int compare)
    {
        
        // try to get the value of the flag, return if it is invalid
        if (!_numberFlags.TryGetValue(flag, out int value)) {
            throw new System.Exception($"[FlagManager] Flag {flag} does not have a value!"); 
        } 

        // check and return the boolean based on the condtion 
        switch (condition) {
        case FlagConditions.flagEQ:
            return (value == compare);
        case FlagConditions.flageNEQ:
            return (value != compare); 
        case FlagConditions.flageGT:
            return (value > compare); 
        case FlagConditions.flagGTE:
            return (value >= compare);
        case FlagConditions.flagLT:
            return (value < compare);
        case FlagConditions.flagLTE:
            return (value <= compare); 
        default:
            throw new System.Exception($"[FlagManager] Invalid condition for number comparison!"); 
        }
    }

    // overriden method for the boolean one, just returns the boolean
    public bool DetermineFlag(string flag, FlagConditions wanted)
    {   
        // if the dictionary has the boolean return whether or not it is what flag is wanted 
        if (_booleanFlags.TryGetValue(flag, out bool value)) {

            // get the wanted as a boolean depending on what the enum says
            bool wantedBool = (wanted == FlagConditions.flagTrue); 
            
            // not XOR will return true if value and wanted are the same, and false if they are differnte
            return !(value ^ wantedBool);

        // otherwise throw an exception 
        } else {
            throw new System.Exception($"[FlagManager] Flag {flag} does not have a value!"); 
        }
    }

    //--------
    //setters
    //--------

    public void StartQuest(string quest)
    {
        _questManager.AddQuest(quest); 
    }

    public void CompleteQuest(string quest)
    {
        _questManager.RemoveQuest(quest); 
    }


}