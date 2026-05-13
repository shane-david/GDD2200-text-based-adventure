using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class EndSceneManager : BaseSceneManager
{

    public TMP_Text EndText; 
    public AudioClip EndMusicGood;
    public AudioClip EndMusicBad;

    public override void Instantiate()
    {
        Name = "EndScreen"; 
        Debug.Log("Insantiating End Scene"); 

        // set the text according to the proper ending
        if (FlagManager.Instance.DetermineFlag("shameEnding", FlagConditions.flagTrue))
        {
            EndText.text = "Skamfararferð Shameful Return"; 
            SoundManager.Instance.ChangBackgroundMusic(EndMusicBad); 
        } else if (FlagManager.Instance.DetermineFlag("sacrificeEnding", FlagConditions.flagTrue))
        {
            EndText.text = "Hetja Fórn A Hero's Sacrifice"; 
            SoundManager.Instance.ChangBackgroundMusic(EndMusicGood); 
        } else if (FlagManager.Instance.DetermineFlag("trueEnding", FlagConditions.flagTrue))
        {
            EndText.text = "Óbrjótanligr vilji Unbreakable Will"; 
            SoundManager.Instance.ChangBackgroundMusic(EndMusicGood); 
        }

    }
}