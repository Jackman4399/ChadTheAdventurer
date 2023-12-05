using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Player {

    public event Action<int> OnLivesChanged;

    [SerializeField] private int m_maxLives = 5;
    public int maxLives => m_maxLives;
    //Purely for testing
    private int currentLives;

    [SerializeField] private float pushbackForce = 500;
    

    [SerializeField] private Color flashColour;
    [SerializeField] private float flashDuration;
    [SerializeField] private int numOfFlashes;
    [SerializeField] private bool invulnerable = false;

    private SpriteRenderer spriteRenderer;

    //NOTE: Use this for death animation or any game end triggers
    //private bool isDead = false;

    protected override void Awake() {
        base.Awake();

        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        //Start with the maximum health
        currentLives = m_maxLives;        
    }

    private void Start() {
        OnLivesChanged?.Invoke(m_maxLives);
    }

    public void Heal(int health) {

        //Heal the player
        currentLives += health;

        //Prevent overhealing
        currentLives = Mathf.Clamp(currentLives, 0, m_maxLives);

        OnLivesChanged?.Invoke(currentLives);
    }

    public void Hurt(int health) {

        if(invulnerable) return;

        //Hurt the player
        currentLives -= health;

        //Prevent negative health
        if(currentLives <= 0) {
            //isDead = true;

        } else {

            StartCoroutine(Flash());

        }


        OnLivesChanged?.Invoke(currentLives);
    }

    //Fetch the player's current HP
    public int GetCurrentHP() {

        return currentLives;

    }
	
    private IEnumerator Flash() {
        invulnerable = true;
        int playerLayer = LayerMask.NameToLayer("Player");
        int monsterLayer = LayerMask.NameToLayer("Enemy");

        Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, true);

        for (int i = 0; i < numOfFlashes; i++) {
            spriteRenderer.color = flashColour;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        Physics2D.IgnoreLayerCollision(playerLayer, monsterLayer, false);

        invulnerable = false;
    }

}
