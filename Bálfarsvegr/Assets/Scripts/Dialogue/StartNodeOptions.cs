using System;
using System.Collections.Generic;
using UnityEngine; 

[System.Serializable]
public struct StartNodeOption
{
    public DialogueNode StartNode; // the node option
    public List<string> Flags;     // the necessary flag for this node optoin to be selected
} 