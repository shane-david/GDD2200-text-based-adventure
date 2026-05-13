// this class represents the boolean flags as a class that is serializable
// since dictionaries are not serializable 
[System.Serializable]
public class BooleanFlag
{
    public string Key;
    public bool   Value; 
}