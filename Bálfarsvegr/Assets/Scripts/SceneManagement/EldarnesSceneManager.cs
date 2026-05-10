using UnityEngine;
using UnityEngine.Playables;

public class EldarnesSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 

    public override void Instantiate()
    {
        Name = "EldarnesMap"; 
        Debug.Log("Insantiating Eldarnes Scene"); 

        // if this is not the first time visiting this scene disable the cut scene
        if (!GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.enabled = false; 
        }

    }
}