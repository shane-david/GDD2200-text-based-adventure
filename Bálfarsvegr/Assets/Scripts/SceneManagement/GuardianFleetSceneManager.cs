using UnityEngine;
using UnityEngine.Playables;

public class GuardianFleetSceneManager : BaseSceneManager
{

    public PlayableDirector ShipCutscene;
    public AudioClip BattleMusic; 
    public override void Instantiate()
    {
        Name = "GuardianFleetMap"; 
        Debug.Log("Insantiating Guradian Fleet Scene"); 

        // if it is the first visit subscribe the the stopped event of the cutscen
        if (GameManager.Instance.IsFirstVisit(Name))
        {
            ShipCutscene.stopped += AfterCutscene; 
        } else {
            ShipCutscene.time = ShipCutscene.duration;
            ShipCutscene.Evaluate();
            ShipCutscene.enabled = false; 
        }

        SoundManager.Instance.ChangBackgroundMusic(BattleMusic); 
    }

    private void AfterCutscene(PlayableDirector director)
    {
        // unsubscirbe
        ShipCutscene.stopped -= AfterCutscene; 

        // go to the narrator scene 
        GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator;
        GameManager.Instance.GoToScene("DialogueScene"); 
    }
}