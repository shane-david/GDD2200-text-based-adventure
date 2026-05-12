using UnityEngine;
using UnityEngine.Playables;

public class MorkreyjarrSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 
    public GameObject YlvaPeopleObject; 

    public override void Instantiate()
    {
        Name = "MorkreyjarMap"; 
        Debug.Log("Insantiating Morkreyjar Scene"); 

        // if this is not the first time visiting this scene disable the cut scene
        if (!GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.time = ShipCutscene.duration; 
            ShipCutscene.Evaluate(); 
            ShipCutscene.enabled = false; 
        }

        // if ylva has been able to help, enable the person to click on to go to scene
        if (FlagManager.Instance !=  null) {

            if (FlagManager.Instance.DetermineFlag("ylvaHelp", FlagConditions.flagTrue) || FlagManager.Instance.DetermineFlag("MorkreyjarNight", FlagConditions.flagTrue)) {
                YlvaPeopleObject.SetActive(true); 
            }

            if (FlagManager.Instance.DetermineFlag("rootCount", FlagConditions.flagGTE, 1) && FlagManager.Instance.DetermineFlag("narratorVoiceCount", FlagConditions.flagLT, 3))
            {
                GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator; 
                GameManager.Instance.GoToScene("DialogueScene");
            }
        }

    }
}