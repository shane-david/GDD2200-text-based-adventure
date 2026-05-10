using UnityEngine;
using UnityEngine.Playables;

public class MorkreyjarrSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 

    public override void Instantiate()
    {
        Name = "MorkreyjarMap"; 
        Debug.Log("Insantiating Morkreyjar Scene"); 

        // if this is not the first time visiting this scene disable the cut scene
        if (!GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.enabled = false; 
        }

    }
}