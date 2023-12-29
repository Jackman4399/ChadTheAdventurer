using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{

    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {

            AudioManager.Instance.PlayOneShot("HeartHeal");

            //Heal player
            other.gameObject.GetComponent<PlayerHealth>().Heal(healAmount);

            //Destroy object once collected
            Destroy(gameObject);

        }

    }
}
