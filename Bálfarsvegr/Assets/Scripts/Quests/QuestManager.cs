using System.Collections.Generic; 
using UnityEngine; 

public class QuestManager
{
    
    private HashSet<string> _activeQuests = new(); 

    public void AddQuest(string quest)
    {   
        Debug.Log($"[QuestManager] {quest} has been started"); 
        _activeQuests.Add(quest); 
    }

    public void RemoveQuest(string quest)
    {
        Debug.Log($"[QuestManager] {quest} has been completed"); 
        _activeQuests.Remove(quest); 
    }

    public bool IsQuestActive(string quest)
    {   
        Debug.Log($"[QuestManager] {quest} status is: {_activeQuests.Contains(quest)}"); 
        return _activeQuests.Contains(quest); 
    }

    public HashSet<string> GetAllQuests()
    {
        return _activeQuests; 
    }
}