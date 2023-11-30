using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class PlayerHealth : Player {

    [SerializeField] private int maxHealth = 100;

    //Purely for testing
    [SerializeField] private int currentHealth;

    //NOTE: Use this for death animation or any game end triggers
    //private bool isDead = false;

    //public AudioSource hurtSound;

    protected override void Awake() {
        base.Awake();

        //Start with the maximum health
        currentHealth = maxHealth;
    }

    public void Heal(int hp) {

        //Heal the player
        currentHealth += hp;

        //Prevent overhealing
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    public void Hurt(int hp) {

        //if(hp > 0) hurtSound.Play();

        //Hurt the player
        currentHealth -= hp;

        //Prevent negative health
        if(currentHealth <= 0) {

            //isDead = true;

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

    //Fetch the player's current HP
    public int GetCurrentHP(){

        return currentHealth;

    }
	

}
