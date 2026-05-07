using UnityEngine;
using UnityEngine.EventSystems; 

// This script is attached to each NPC and handles the player click interactions 
// it contains a reference to the NPC's specific scriptable object, and then 
// just translates the play actions into the scriptable object methods that need to be called
public class UIInteractor : MonoBehaviour, IPointerClickHandler
{
    
    //-------------
    //private data
    //-------------

    [SerializeField] private NPC _npc; 

    //-------------
    //click methods
    //-------------

    // this method is called when a click event occurs 
    public void OnPointerClick(PointerEventData pointerEventData)
    {   

        // set the game manager's current npc 
        GameManager.Instance.CurrentNPC = _npc; 

        // go to the dialouge scene
        GameManager.Instance.GoToScene("DialogueScene"); 

    }

}
