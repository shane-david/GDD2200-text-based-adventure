using UnityEngine;
using UnityEngine.Playables;

public class StartSceneManager : BaseSceneManager
{

    public override void Instantiate()
    {
        Name = "StartScreen"; 
        Debug.Log("Insantiating StartScreen Scene"); 

    }

    public void OnStart()
    {   
        FlagManager.Instance.SetFlag("GameStart", true); 
        FlagManager.Instance.SetFlag("onFornaheim", true); 
        GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator; 
        GameManager.Instance.GoToScene("DialogueScene", "FornaheimMap"); 
    }
}