using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public AudioSource heal;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.layer.Equals("Player")) {

            heal.Play();
            //Destroy object once collected
            Destroy(gameObject);

        }

    }
}
