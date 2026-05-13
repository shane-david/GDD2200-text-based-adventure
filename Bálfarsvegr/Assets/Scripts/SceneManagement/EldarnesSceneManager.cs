using UnityEngine;
using UnityEngine.Playables;

public class EldarnesSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 
    public GameObject YlvaObject; 
    public AudioClip PowerConquestMusic; 

    public override void Instantiate()
    {
        Name = "EldarnesMap"; 
        Debug.Log("Insantiating Eldarnes Scene"); 

        // if this is not the first time visiting this scene disable the cut scene
        if (!GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.time = ShipCutscene.duration; 
            ShipCutscene.Evaluate(); 
            ShipCutscene.enabled = false; 
        }

        // activate the ylva object after the player has talked to ragnava
        if (FlagManager.Instance != null && FlagManager.Instance.DetermineFlag("hasTalkedToRagnavald", FlagConditions.flagTrue))
        {
            YlvaObject.SetActive(true); 
        }

        SoundManager.Instance.ChangBackgroundMusic(PowerConquestMusic); 

    }
}