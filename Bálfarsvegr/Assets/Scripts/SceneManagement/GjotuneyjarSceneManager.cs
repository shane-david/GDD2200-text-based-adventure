using UnityEngine;
using UnityEngine.Playables;

public class GjotuneyjarSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 

    public override void Instantiate()
    {
        Name = "GjotuneyjarMap"; 
        Debug.Log("Insantiating Gjotuneyjar Scene"); 

        // if this is not the first time visiting this scene disable the cut scene
        if (!GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.enabled = false; 
        }

        // if the player has already obtained some loftjarn go to the narrator dialouge 
        // so they can either start the stealth quest or prompt the player to the next locatoin 
        if (FlagManager.Instance.DetermineFlag("hasTalkedToDagr", FlagConditions.flagTrue)) {
            GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator; 
            GameManager.Instance.GoToScene("DialogueScene"); 
        }
    }
}