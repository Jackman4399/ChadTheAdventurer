using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class PlayerHealth : Player {

    [SerializeField] private int maxHealth = 5;

    //Purely for testing
    [SerializeField] private int currentHealth;

    [SerializeField] private Color flashColour, regularColour;

    [SerializeField] private float flashDuration;
    [SerializeField] private int numOfFlashes;
    [SerializeField] private bool invulnerable = false;

    [SerializeField] private SpriteRenderer renderer;

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

        if(invulnerable) return;

        //Hurt the player
        currentHealth -= hp;

        //Prevent negative health
        if(currentHealth > 0) {

            if(hp > 0) FindObjectOfType<AudioManager>().Play("PlayerHit");
            StartCoroutine(Flash());

        } else {
            //isDead = true;
        }

    }

    //Fetch the player's current HP
    public int GetCurrentHP(){

        return currentHealth;

    }

    private IEnumerator Flash() {
        
        int temp = 0;
        invulnerable = true;
        while (temp < numOfFlashes) {
            
            renderer.color = flashColour;
            yield return new WaitForSeconds(flashDuration);
            renderer.color = regularColour;
            yield return new WaitForSeconds(flashDuration);
            temp++;

        }
        invulnerable = false;

    }
	

}
