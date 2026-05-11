using System;
using NUnit.Framework.Constraints;
using TMPro;
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
    [SerializeField] private string _nextScene; 

    [Header("FLAG TO SET IF ONE")]
    [SerializeField] private SetFlag _flagToSet; 

    //-------------
    //click methods
    //-------------

    // this method is called when a click event occurs 
    public void OnPointerClick(PointerEventData pointerEventData)
    {   

        // if verything is not set throw an error and return
        if (_npc == null && string.IsNullOrEmpty(_nextScene) && _flagToSet == null)
        {
            throw new System.Exception("[UIHandler] ERROR: must fill ONE field in UIhandler"); 
        }

        if (_npc != null) {
            // set the game manager's current npc 
            GameManager.Instance.CurrentNPC = _npc; 

            // go to the dialouge scene
            GameManager.Instance.GoToScene("DialogueScene"); 
        } else if (!string.IsNullOrEmpty(_nextScene)) {
            
            // go to the specified scene
            GameManager.Instance.GoToScene(_nextScene); 

        } else if (_flagToSet != null) {
            
            // if it is a number flag it will be increase or decrese
            if (_flagToSet.FlagChange == FlagChanges.increase) {
                FlagManager.Instance.SetFlag(_flagToSet.name, _flagToSet.ChangeAmount);
            } else if (_flagToSet.FlagChange == FlagChanges.decrease) {
                FlagManager.Instance.SetFlag(_flagToSet.name, -1 * _flagToSet.ChangeAmount);

            // otherwise it is set true or set false
            } else {
                FlagManager.Instance.SetFlag(_flagToSet.name, _flagToSet.FlagChange == FlagChanges.setTrue); 
            }
        } 

    }

}
