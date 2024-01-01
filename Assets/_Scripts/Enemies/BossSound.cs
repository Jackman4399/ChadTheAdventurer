using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour {
    
    public void PlayMelee(){
        AudioManager.Instance.PlayOneShot("Boss_Melee");
    }

    public void PlayRanged(){
        AudioManager.Instance.PlayOneShot("Boss_Ranged");
    }

    public void PlayLaser(){
        AudioManager.Instance.PlayOneShot("Boss_Laser");
    }

}