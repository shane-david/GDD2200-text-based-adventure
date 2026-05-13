using UnityEngine;

public class BreakingFleetCutscene : MonoBehaviour
{   

    public GameObject BrokenShips; 
    public AudioClip ShipCrash; 

    //----------------------
    //Unity Lifetime Methods
    //----------------------

    // thid method checks for the ship collision and then sets itself ot fals after
    // activating the broken one 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            SoundManager.Instance.PlaySFX(ShipCrash); 
            gameObject.SetActive(false);
            BrokenShips.SetActive(true); 
        }
    }
}
