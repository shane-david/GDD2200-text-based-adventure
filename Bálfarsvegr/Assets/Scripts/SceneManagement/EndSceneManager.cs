using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class EndSceneManager : BaseSceneManager
{

    public TMP_Text EndText; 

    public override void Instantiate()
    {
        Name = "EndScene"; 
        Debug.Log("Insantiating End Scene"); 

        // set the text according to the proper ending
        if (FlagManager.Instance.DetermineFlag("shameEnding", FlagConditions.flagTrue))
        {
            EndText.text = "Skamfararferð Shameful Return"; 
        } else if (FlagManager.Instance.DetermineFlag("sacrificeEnding", FlagConditions.flagTrue))
        {
            EndText.text = "Hetja Fórn A Hero's Sacrifice"; 
        } else if (FlagManager.Instance.DetermineFlag("trueEnding", FlagConditions.flagTrue))
        {
            EndText.text = "Óbrjótanligr vilji Unbreakable Will"; 
        }
    }
}