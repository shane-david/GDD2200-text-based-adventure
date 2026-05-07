using UnityEngine;

public class GjotuneyjarSceneManager : BaseSceneManager
{

    public override void Instantiate()
    {
        Name = "GjotuneyjarMap"; 
        Debug.Log("Insantiating Gjotuneyja Scene"); 

        // if the player enters the scene and they have the Loftjanr Stealth quest active
        // the narrator should be prompted to do his Loftjarn Stealth nodes
        GameManager.Instance.Narrator.StartDialogue(); 
    }
}