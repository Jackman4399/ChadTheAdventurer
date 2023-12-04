using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{

    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D other) {
        
        //Hurt player if true
        if(other.gameObject.layer == 10){

            Debug.Log("Attacking player!");

            other.gameObject.GetComponent<PlayerHealth>().Hurt(damage);

        }

    }

}
