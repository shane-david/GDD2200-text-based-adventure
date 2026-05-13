// this class stores all of the data that needs to be saved and written to a json save file by unity
// right now it contains the number and boolean flags, the quests, and the scene that will be loaded
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{   

    // version data
    public string Version = "v1.0";
    public string LastSaved; 
    
    // game data
    public List<NumberFlag>  NumberFlags;
    public List<BooleanFlag> BooleanFlags;
    public List<string>      Quests;
    public string            CurrentScene; 
}