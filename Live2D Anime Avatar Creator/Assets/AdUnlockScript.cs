using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdUnlockScript : MonoBehaviour
{
    public bool Unlocked = false;

    public void Unlock()
    {
        if(Unlocked == false)
        {
            //show pop-up do you want to watch ad
                //if yes, put adsintitalizerscript on the yes button to immediately run the ad
                //if not, remove pop up
        }
    }

}
