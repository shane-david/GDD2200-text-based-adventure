using System;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; 

// This script is attached to each NPC and handles the player click interactions 
// it contains a reference to the NPC's specific scriptable object, and then 
// just translates the play actions into the scriptable object methods that need to be called
public class UIInteractor : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    //-------------
    //private data
    //-------------

    [SerializeField] private NPC _npc; 
    [SerializeField] private string _nextScene; 

    [Header("FLAG TO SET IF ONE")]
    [SerializeField] private SetFlag _flagToSet; 

    [Header("Hover information")]
    [SerializeField] private string _hoverTitle;
    [SerializeField] private string _hoverDescription;
    [SerializeField] private GameObject _hoverPanelPrefab; 

    private GameObject _activeHover; 

    //-------------
    //click methods
    //-------------

    // this method is called when a click event occurs 
    public void OnPointerClick(PointerEventData pointerEventData)
    {   

        Debug.Log(name); 
        
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

    // this method will be called when the player hovers over the object, it will show 
    // a panel with a short description and title of what will happen when it is clicked
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        
        // throw an error if the title or description are emtpy
        if (string.IsNullOrEmpty(_hoverDescription) || string.IsNullOrEmpty(_hoverTitle))
        {
            throw new System.Exception("[UIInteractor] ERROR: must fill in title and descriptoin!"); 
        }

        // throw an error if the prefab is missing
        if (_hoverPanelPrefab == null)
        {
            throw new System.Exception("[UIInteractor] ERROR: must fill in hover panel prefab"); 
        }

        // do not spawn if there is alreayd a pane'
        if (_activeHover != null) return; 

        // insantiate the hover
        Canvas canvasToSpawn = FindFirstObjectByType<Canvas>(); 
        _activeHover = Instantiate(_hoverPanelPrefab, canvasToSpawn.transform); 

        RectTransform tooltipRect = _activeHover.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasToSpawn.GetComponent<RectTransform>(),
            pointerEventData.position,
            null,
            out Vector2 localPoint
        );

        tooltipRect.anchoredPosition = new Vector2(localPoint.x, localPoint.y + 20f);

        // get and set the title tex
        _activeHover.transform.Find("Title").GetComponent<TMP_Text>().text = _hoverTitle; 

        // get and set the description text
        _activeHover.transform.Find("Description").GetComponent<TMP_Text>().text = _hoverDescription; 

    }

    // this method will be called when the player stops hovering over the object, it just 
    // destrys the panle
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        
        // if there is an active hover
        if (_activeHover != null)
        {
            
            // destory it 
            Destroy(_activeHover);
            _activeHover = null; 
        }
    }

}
