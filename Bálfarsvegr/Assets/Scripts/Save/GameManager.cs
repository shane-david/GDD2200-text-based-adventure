using System.Collections.Generic;
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

    public BaseSceneManager CurrentScene; 
    [HideInInspector] public string NextScene; 
    private HashSet<string> _visitedScenes = new(); 

    //-----------------------
    //Unity Lifetime Methods
    //-----------------------

    protected override void Awake()
    {
        base.Awake(); 

        if (Instance != this) return; 

        SetCurrentScene(); 
        CurrentScene.Instantiate(); 
    }

    //---------------
    //public methods
    //---------------
    public void GoToScene(string sceneName, string nextScene = null)
    {

        // if the caller did not specify the next scene, just make it the current scene
        // so that we can return here after the dialogue scene, if they did specify, set it 
        NextScene = (nextScene == null) ? FindFirstObjectByType<BaseSceneManager>().Name : nextScene; 
        

        // make sure the scene manager is found
        if (NextScene == null)
        {
            throw new System.Exception("[GameManager] No SceneManager found in scene!"); 
        }

        // subscribe the on scene loaded event to SceneManager
        SceneManager.sceneLoaded -= OnSceneLoaded; 
        SceneManager.sceneLoaded += OnSceneLoaded; 

        // go to the scene
        SceneManager.LoadScene(sceneName); 

    }

    // if it is the first visit add it the hash map 
    // if it is not the first visit return false 
    public bool IsFirstVisit(string sceneName)
    {
        return _visitedScenes.Add(sceneName); 
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