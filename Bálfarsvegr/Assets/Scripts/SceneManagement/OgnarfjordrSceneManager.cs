using UnityEngine;
using UnityEngine.Playables;

public class OgnarfjordrSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 

    public override void Instantiate()
    {
        Name = "OgnarfjordrMap"; 
        Debug.Log("Insantiating Ognarfjordr Scene"); 

        // if this is not the first time visiting this scene disable the cut scene
        if (!GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.time = ShipCutscene.duration; 
            ShipCutscene.Evaluate(); 
            ShipCutscene.enabled = false; 
        }

    }
}