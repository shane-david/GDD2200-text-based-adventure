using UnityEngine; 

public enum FlagChanges {increase, decrease, setTrue, setFalse}; 

[System.Serializable]

public class SetFlag : Flag
{
    public string name;
    public FlagChanges FlagChange; 
    public int ChangeAmount;
}