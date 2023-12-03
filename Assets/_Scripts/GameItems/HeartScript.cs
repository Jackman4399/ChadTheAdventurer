using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public AudioSource heal;

    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.layer.Equals("Player")) {

            heal.Play();

            //Heal player
            other.gameObject.GetComponent<PlayerHealth>().Heal(healAmount);

            //Destroy object once collected
            Destroy(gameObject);

        }

    }
}
