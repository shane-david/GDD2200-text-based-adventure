using System;
using System.IO;
using UnityEngine;

// this is a static class that handles the saving and loading
// when saving it takes just creates a save data and writes it to the save file
// when loading it reads from the save file and returns the save data class for the start screen to pars
public static class SaveManager
{
    
    // the path to the save file 
    private static readonly string PathToFile = Application.persistentDataPath + "/GameSaveFile.json"; 

    // the save method does not need to recieve anything, all of the data the needs to be saved 
    // exists on singletons (flags, quests, current scene), so it just uses those singletones
    // to create its own save data and then write it to the json file 
    public static void Save()
    {
        
        // create the save data using the singletone we have in place
        SaveData data = new SaveData
        {   
            Version = Application.version,
            LastSaved = DateTime.UtcNow.ToString("o"),
            NumberFlags = FlagManager.Instance.NumberFlagsToList(),
            BooleanFlags = FlagManager.Instance.BooleanFlagsToList(),
            Quests = FlagManager.Instance.QuestsToList(),
            CurrentScene = GameManager.Instance.CurrentScene.Name
        }; 

        // transform the data into json using hte Json utility 
        string json = JsonUtility.ToJson(data, true); 

        // write the Json to the save file 
        File.WriteAllText(PathToFile, json); 

        // log it 
        Debug.Log($"[SaveManager] saved game data to {PathToFile}");
    }

    // the load method reads in the json file from the save path and loads it into
    // a SaveData instance, it returns that instance so that the StartSceneManager 
    // can parse it into is proper singletons once the start button is pressed
    public static SaveData Load()
    {
        
        // return null if there is no save data, this tells the start scene manager 
        // to just load the game as if it is the first time playing 
        if (!File.Exists(PathToFile))
        {
            return null; 
        }

        // get the file back into a json string
        string json = File.ReadAllText(PathToFile); 

        // log the load
        Debug.Log($"[SaveManager] loaded game data from {PathToFile}");

        // return the the save data
        return JsonUtility.FromJson<SaveData>(json); 
    }

    // delete the save for when tyring to go for another ending
    public static void DeleteSave()
    {
        if (File.Exists(PathToFile))
        {
            File.Delete(PathToFile); 
            Debug.Log("[SaveManager] File Deleted"); 
        }
    }
}