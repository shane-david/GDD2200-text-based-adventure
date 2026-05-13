using UnityEngine;

// this is the abstract class for a singleton, anything that needs to persist 
// can inherit from this class and define its singleton type instead of monobehavior
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    
    //----------------
    //Singleton Logic
    //----------------

    public static T Instance { get; private set; }
    
    // create the static instance if it does not exist yet 
    protected virtual void Awake()
    {   
        // if the Instance has already been instantiated, destroy this game object and continue with the old one
        if (Instance != null) {

            Destroy(gameObject); 
            return;

        // otherwise instantiate the object and mark it as do not destroy on load 
        } else {
            
            Instance = (T)(MonoBehaviour)this; 
            DontDestroyOnLoad(gameObject); 
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; 
        }
    }
}