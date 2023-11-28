using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{

    [SerializeField] private int damage = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        
        //Hurt player if true
        if(other.gameObject.layer.Equals("Player")){

            other.gameObject.GetComponent<PlayerHealth>().Hurt(damage);

        }

    }

}
