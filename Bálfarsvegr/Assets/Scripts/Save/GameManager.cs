using UnityEngine; 
using UnityEngine.SceneManagement; 

// this class handles data and transtions that are global to the game
public class GameManager : Singleton<GameManager>
{

    //-------------
    //public data
    //-------------
    [HideInInspector] public NPC CurrentNPC; 
    public NPC Narrator; 

    private BaseSceneManager CurrentScene; 
    [HideInInspector] public BaseSceneManager PreviousScene; 

    //-----------------------
    //Unity Lifetime Methods
    //-----------------------

    protected override void Awake()
    {
        base.Awake(); 
        SetCurrentScene(); 
        CurrentScene.Instantiate(); 
    }

    //---------------
    //public methods
    //---------------
    public void GoToScene(string sceneName)
    {

        // keep track of the previous scene so we can return 
        PreviousScene = FindFirstObjectByType<BaseSceneManager>(); 

        // make sure the scene manager is found
        if (PreviousScene == null)
        {
            throw new System.Exception("[GameManager] No SceneManager found in scene!"); 
        }

        // go to the scene
        SceneManager.LoadScene(sceneName); 

        // subscribe the on scene loaded event to SceneManager
        SceneManager.sceneLoaded += OnSceneLoaded; 

    }

    //----------------
    //private methods
    //----------------

    // once the scene is loaded insantiate the current scne
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        // unsubscribe for the SceneManager event
        SceneManager.sceneLoaded -= OnSceneLoaded; 
        
        // set the current scne
        SetCurrentScene(); 
        
        // instantiate the scene
        CurrentScene.Instantiate(); 
    }

    private void SetCurrentScene()
    {
        // get the current scene from the game object
        CurrentScene = FindFirstObjectByType<BaseSceneManager>(); 

        // make sure the scene manager is found
        if (CurrentScene == null)
        {
            throw new System.Exception("[GameManager] No SceneManager found in scene!"); 
        }
    }
}