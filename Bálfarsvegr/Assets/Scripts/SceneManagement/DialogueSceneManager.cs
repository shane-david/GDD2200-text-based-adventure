using System;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSceneManager : BaseSceneManager
{

    //--------------
    //private data  
    //--------------

    [SerializeField] private TMP_Text     _dialogueText;
    [SerializeField] private List<Button> _choiceButtons; 

    NPC _npc; 

    // override instantiate to set up the dialogue scene 
    public override void Instantiate()
    {
        Debug.Log("Instantiating the Dialogue Scene..."); 

        // set the name
        Name = "DialogueScene"; 

        // cache the NPC 
        _npc = GameManager.Instance.CurrentNPC; 

        // instantite the current NPC prefab so they can talk 
        UnityEngine.Object.Instantiate(_npc.GetPrefab(), new Vector2(5,-2), Quaternion.identity); 

        // start the current npcs dialogue
        _npc.StartDialogue(); 

        // update the dialoauge
        UpdateDialogue(); 
    }

    //---------------
    //private methods
    //---------------

    // method to update the dialouge text
    public void UpdateDialogue()
    {   
        
        // update the dialogue 
        _dialogueText.text = _npc.GetDialogue(); 

        // get the list of choices
        List<string> choiceText = _npc.GetChoiceStrings(); 

        // iterate through the choice strings and update the buttons 
        int i = 0;
        for (; i < choiceText.Count; i++)
        {   
            _choiceButtons[i].gameObject.SetActive(true); 
            _choiceButtons[i].GetComponentInChildren<TMP_Text>().text = choiceText[i]; 
        }

        // if i is not at the lenght of the choice text, disable the button 
        for (; i < _choiceButtons.Count; i++)
        {
            _choiceButtons[i].gameObject.SetActive(false); 
        }
    }

    //---------------
    //public methods
    //---------------

    public void ActivateChoice(int index)
    {   
        
        // set the flags for that activated choice
        _npc.GetChoice(index).SetFlags(); 

        // if you can go next, update the dialogue
        if (_npc.GetChoice(index).TryGoNext(_npc)) {

            UpdateDialogue(); 

        // otherwise, return to the npcs home 
        } else {
            GameManager.Instance.GoToScene(GameManager.Instance.PreviousScene.Name); 
        }
    }

}