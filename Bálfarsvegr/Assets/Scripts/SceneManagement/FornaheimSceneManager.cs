using UnityEngine;

public class FornaheimSceneManager : BaseSceneManager
{

    public override void Instantiate()
    {
        Name = "FornaheimMap"; 
        Debug.Log("Insantiating Fornaheim Scene"); 

        if (FlagManager.Instance != null)
        {
            
            // do the return home narrator message if questReturnHome is active 
            if (FlagManager.Instance.IsQuestActive("questReturnHome"))
            {
                GameManager.Instance.CurrentNPC = GameManager.Instance.Narrator; 
                GameManager.Instance.GoToScene("DialogueScene");
            }
        }
    }
}