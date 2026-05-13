using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    
    public void Exit()
    {   
        SaveManager.Save(); 
        Application.Quit();
    }

    public void StartNewGame()
    {
        SaveManager.DeleteSave();
        Destroy(FlagManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject); 
        SceneManager.LoadScene("StartScreen"); 
    }
}