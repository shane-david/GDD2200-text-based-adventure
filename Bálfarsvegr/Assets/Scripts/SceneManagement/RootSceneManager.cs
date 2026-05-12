using UnityEngine;
using UnityEngine.Playables;

public class RootSceneManager : BaseSceneManager
{

    public GameObject YlvaObject; 

    public override void Instantiate()
    {
        Name = "RootScene"; 
        Debug.Log("Insantiating Root Scene"); 

        // only display ylva once the player has grabbed the root
        if (FlagManager.Instance.DetermineFlag("rootCount", FlagConditions.flagGTE, 1))
        {
            YlvaObject.SetActive(true); 
        }
    }
}