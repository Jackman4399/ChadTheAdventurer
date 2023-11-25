using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player {

    public int maxHealth = 100;

    private int currentHealth;

    //NOTE: Use this for death animation or any game end triggers
    private bool isDead = false;

    private void Start() {

        //Start with the maximum health
        currentHealth = maxHealth;
    }

    public void Heal(int hp) {

        //Heal the player
        currentHealth += hp;

        //Prevent overhealing
        if(currentHealth > maxHealth) {

            currentHealth = maxHealth;

        }

    }

    public void Hurt(int hp) {

        //Hurt the player
        currentHealth -= hp;

        //Prevent negative health
        if(currentHealth <= 0) {

            isDead = true;

        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        //Unique to the green slime, could be moved to enemy's side
        //Or extended to differentiate dmg from different enemies
        if (other.gameObject.layer.Equals("GreenSlime")) {

            //Take dmg
            Hurt(10);

        }

    }
	

}
