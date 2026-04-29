using UnityEngine; 

public enum FlagConditions { flagTrue, flagFalse, flageGT, flagLT, flagGTE, flagLTE, flagEQ, flageNEQ}

[System.Serializable]
public class ConditionFlag : Flag
{
    public string name;
    public FlagConditions FlagCondition; 
    public int Compare; 
}