using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{

    public AudioSource bgm;

    //Manually pause the bgm. To be used by game manager
    public void Pause() {

        if(bgm != null) {
           bgm.Pause(); 
        }
        
    }

    //Manually play the bgm. To be used by game manager
    public void Play() {

        if(bgm != null) {
           bgm.Play(); 
        }
        
    }


}
