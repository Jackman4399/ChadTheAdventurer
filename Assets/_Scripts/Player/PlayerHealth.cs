using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class PlayerHealth : Player {

    [SerializeField] private int maxHealth = 5;

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

        Debug.Log("Player got hurt!");

        //if(hp > 0) hurtSound.Play();

        //Hurt the player
        currentHealth -= hp;

        //Prevent negative health
        if(currentHealth <= 0) {

            //isDead = true;

        }

    }

    //Fetch the player's current HP
    public int GetCurrentHP(){

        return currentHealth;

    }
	

}
