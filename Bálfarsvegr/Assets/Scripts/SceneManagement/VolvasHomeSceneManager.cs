using UnityEngine;
using UnityEngine.Playables;

public class VolvasHomeSceneManager : BaseSceneManager
{

    public GameObject VindstafrObject; 

    public override void Instantiate()
    {
        Name = "VolvasHome"; 
        Debug.Log("Insantiating VolvasHome Scene"); 

        // if vindstafr has been taken do not show it 
        if (FlagManager.Instance != null && FlagManager.Instance.DetermineFlag("vindstafrCount", FlagConditions.flagGTE, 1))
        {
            VindstafrObject.SetActive(false); 
        }
    }
}