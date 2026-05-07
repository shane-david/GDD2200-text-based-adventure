using System.Collections.Generic; 

public class QuestManager
{
    
    private HashSet<string> _activeQuests; 

    public void AddQuest(string quest)
    {
        _activeQuests.Add(quest); 
    }

    public void RemoveQuest(string quest)
    {
        _activeQuests.Remove(quest); 
    }

    public bool isQuestActive(string quest)
    {
        return _activeQuests.Contains(quest); 
    }
}