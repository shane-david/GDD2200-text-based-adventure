using UnityEngine;
using UnityEngine.Playables;

public class StartSceneManager : BaseSceneManager
{

    public AudioClip StartingMusic;

    public override void Instantiate()
    {
        Name = "StartScreen"; 
        Debug.Log("Insantiating StartScreen Scene"); 
        SoundManager.Instance.ChangBackgroundMusic(StartingMusic); 
    }

    // if it is the first time starting the game go to
    // the starting narrator dialgoue 
    private void OnStart()
    {   
        FlagManager.Instance.SetFlag("GameStart", true); 
        FlagManager.Instance.SetFlag("onFornaheim", true); 
        GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator; 
        GameManager.Instance.GoToScene("DialogueScene", "FornaheimMap"); 
    }

    // this method tries to load the save, if there is not save file it just starts
    // the game as usual, if ther is it loads the save and goes to the current scene 
    public void TryLoadSave()
    {
        
        // get the save data from the SaveManager
        SaveData data = SaveManager.Load(); 

        // if is is null start the game fresh
        if (data == null)
        {
            OnStart();
            return; 
        }

        // otherwise set the flag manager data 
        FlagManager.Instance.SetNumberFlags(data.NumberFlags);
        FlagManager.Instance.SetBooleanFlags(data.BooleanFlags); 
        FlagManager.Instance.SetAllQuests(data.Quests); 

        // go to the current scene from the manager
        GameManager.Instance.GoToScene(data.CurrentScene); 
    }
}