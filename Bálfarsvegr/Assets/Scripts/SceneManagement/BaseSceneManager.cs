using UnityEngine;

public abstract class BaseSceneManager : MonoBehaviour
{
    
    // string for the name that can be loaded from with SceneManager
    [HideInInspector] public string Name; 

    // abstract method to set up a scene
    public abstract void Instantiate(); 

}