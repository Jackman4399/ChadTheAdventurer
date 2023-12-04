using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Player {

    public static event Action<int> OnLivesChanged;
    public static event Action OnHit;

    [SerializeField] private int initialLives = 5;
    //Purely for testing
    private int currentLives;

    [SerializeField, Tooltip("How many seconds does invincibility applies to player after getting hit")]
    private float hitInvincibleTime = 2.5f;

    [SerializeField] private float pushbackForce = 500;
    

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
        currentLives = initialLives;

        OnLivesChanged?.Invoke(initialLives);
    }

    public void Heal(int health) {

        //Heal the player
        currentLives += health;

        //Prevent overhealing
        currentLives = Mathf.Clamp(currentLives, 0, initialLives);

        OnLivesChanged?.Invoke(currentLives);
    }

    public void Hurt(int health) {

        if(invulnerable) return;

        //Hurt the player
        currentLives -= health;

        //Prevent negative health
        if(currentLives <= 0) {

        } else {
            //isDead = true;
        }

        OnLivesChanged?.Invoke(currentLives);
    }

    //Fetch the player's current HP
    public int GetCurrentHP() {

        return currentLives;

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
