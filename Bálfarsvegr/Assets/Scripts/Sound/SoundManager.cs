using UnityEngine;
using UnityEngine.Experimental.Audio;

public class SoundManager : Singleton<SoundManager>
{
    
    //-------------
    //private data
    //-------------

    [SerializeField] private AudioSource _sfxSource; 

    
    //---------------
    //public methods
    //---------------

    // method to play a clip, called in various places 
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip); 
    }

    // method to start the background music, should be called in the scene manager 
    // if it should change and the background music will just continue if not 
    public void ChangBackgroundMusic(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.loop = true; 
        _sfxSource.Play(); 
    }

}