using UnityEngine;

public class ForlagseySceneManager : BaseSceneManager
{

    public GameObject FireOnBoat;
    public override void Instantiate()
    {
        Name = "ForlagseyMap"; 
        Debug.Log("Insantiating Forlagsy Scene"); 

        if (FlagManager.Instance.DetermineFlag("didBurnBoats", FlagConditions.flagFalse))
        {
            FireOnBoat.SetActive(false); 
        }
    }
}