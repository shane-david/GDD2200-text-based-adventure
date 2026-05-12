using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class UIToggle : MonoBehaviour
{
    
    //------------
    //private data
    //------------

    [SerializeField] private RectTransform _UIPanel; 
    [SerializeField] private GameObject _questSection;
    [SerializeField] private TMP_Text   _questList; 
    [SerializeField] private GameObject _statsSection; 
    [SerializeField] private TMP_Text   _statsText; 
    [SerializeField] private GameObject _inventorySection; 
    [SerializeField] private TMP_Text   _inventoryText; 


    private bool  _isOpen = true; 
    private float _panelWidth; 

    //-----------------------
    //Unity Lifetime Methods
    //-----------------------

    private void Awake() {


        // cache the pane width 
        _panelWidth = _UIPanel.rect.width; 

        // start the thing as not open
        _isOpen = true;
        OnPanelToggle(); 

    }

    //---------------
    //private methods
    //---------------

    private void RenderQuests()
    {
        
        // get all the quest from the flag manager
        var activeQuests = FlagManager.Instance.GetQuests(); 
        string questList = ""; 

        foreach (var quest in activeQuests)
        {   
            // format the string to appear nice on the screen and add it to the list 
            questList += FormatQuestName(quest) + '\n'; 
        }

        _questList.text = questList; 
    }

    private string FormatQuestName(string orignalName)
    {
        
        // strip the quest prefix off of the stin
        string stripped = Regex.Replace(orignalName, "^quest", "", RegexOptions.IgnoreCase); 

        // insert a space before each capital letter that denotes each word
        string spaced = Regex.Replace(stripped, "(?<!^)([A-Z])", " $1"); 

        return char.ToUpper(spaced[0]) + spaced.Substring(1); 
    }

    private void RenderStats()
    {

        string statsText = ""; 

        // add current conquest to stats
        if (FlagManager.Instance.DetermineFlag("resourceConquest", FlagConditions.flagTrue) || FlagManager.Instance.DetermineFlag("powerConquest", FlagConditions.flagTrue)) {
            statsText += "Current Conquest: "; 
            statsText += (FlagManager.Instance.DetermineFlag("resourceConquest", FlagConditions.flagTrue) ? "Resource Conquest" : "Power Conquest"); 
        } 

        // add humanity score to stats
        statsText += '\n'; 
        statsText += "Humanity Score: " + FlagManager.Instance.GetHumanityScore(); 

        // set the texxt
        _statsText.text = statsText; 
    }

    private void RenderInventory()
    {
        
        // start with a base inventory text
        string inventoryText = ""; 

        // get the number flags from the flag manager
        var possibleItems = FlagManager.Instance.GetNumberFlags(); 

        // iterate through number flags
        foreach (var item in possibleItems)
        {
            
            // if the flag ends in count it is an item
            if (item.Key.EndsWith("Count") && item.Key != "narratorVoiceCount")
            {
                inventoryText += FormatItemName(item.Key) + ": " + item.Value + '\n'; 
            }
        }

        _inventoryText.text = inventoryText; 

    }

    private string FormatItemName(string item)
    {
        // strip count from the end
        string stripped = item.Substring(0, item.Length - 5); 

        // formate it in the same way we did quest names
        return FormatQuestName(stripped); 
    }

    //---------------
    //public methods
    //---------------

    public void OnPanelToggle() {

        // set is open to the opposite bool
        _isOpen = !_isOpen; 

        // if it is open the position is 0 otherwise we slide it over by the panel widht
        float targeX = _isOpen ? 0f : _panelWidth; 

        // move he quest panel to its proper location 
        _UIPanel.anchoredPosition = new Vector2(targeX, _UIPanel.anchoredPosition.y); 
    }

    public void SetSection(string section)
    {
        
        // if it is not open call OnPanetoggle
        if (!_isOpen)
        {
            OnPanelToggle(); 
        }

        // set all sections false
        _questSection.SetActive(false); 
        _statsSection.SetActive(false); 
        _inventorySection.SetActive(false); 

        // set the passed in sectionr to true 
        if (section == "quest")
        {
            _questSection.SetActive(true); 
            RenderQuests(); 
        } else if (section == "stats")
        {
            _statsSection.SetActive(true); 
            RenderStats(); 
        } else if (section == "inventory")
        {
            _inventorySection.SetActive(true); 
            RenderInventory(); 
        }
    }
}
