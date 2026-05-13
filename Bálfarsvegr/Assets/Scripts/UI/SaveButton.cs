using UnityEngine;

// this class is the caller of the save manager when the save button is clicked
public class SaveButton : MonoBehaviour
{
    
    public void Save()
    {
        SaveManager.Save(); 
    }

    public void SaveAndQuit()
    {
        SaveManager.Save();
        Application.Quit(); 
    }

    public void DeleteSave()
    {
        SaveManager.DeleteSave(); 
    }
}