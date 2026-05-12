using UnityEngine;
using UnityEngine.Playables;

public class OgnarfjordrSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene; 
    public GameObject TornadoObject; 

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

        // once the player has talked to Volva, enable the tornado
        if (FlagManager.Instance != null && FlagManager.Instance.DetermineFlag("hasTalkedToVolva", FlagConditions.flagTrue))
        {
            TornadoObject.SetActive(true); 
        }

        
        // if the player is done with volva go to the narrator line 
        if (FlagManager.Instance !=  null) {

            if (FlagManager.Instance.DetermineFlag("isVindstafrEnabled", FlagConditions.flagTrue) && FlagManager.Instance.DetermineFlag("narratorVoiceCount", FlagConditions.flagLT, 4)) {
                GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator; 
                GameManager.Instance.GoToScene("DialogueScene");
            }
        }

    }
}