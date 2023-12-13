using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Player {

    public event Action<int> OnLivesChanged;
    public event Action OnHit, OnDied;

    [SerializeField] private int maxLives = 5;
    public int MaxLives => maxLives;
    //Purely for testing
    private int currentLives;
    
    [SerializeField, Tooltip("How long should the player be invulnerable once hit in seconds.")]
    private float hitInvulnerableTime = 2.5f;
    public float HitInvulnerableTime => hitInvulnerableTime;

    //NOTE: Use this for death animation or any game end triggers
    //private bool isDead = false;

    protected override void Awake() {
        base.Awake();

        //Start with the maximum health
        currentLives = maxLives;
    }

    public void Heal(int lives) {
        //Heal the player
        currentLives += lives;

        //Prevent overhealing
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);

        OnLivesChanged?.Invoke(currentLives);
    }

    public void Hurt(int lives) {
        //Hurt the player
        currentLives -= lives;
        OnLivesChanged?.Invoke(currentLives);

        //Prevent negative health
        if(currentLives == 0) OnDied?.Invoke();
        else {
            OnHit?.Invoke();
            StartCoroutine(HitInvunerableCoroutine());
        }

        //StartCoroutine(Flash());
        
    }

    //Fetch the player's current HP
    public int GetCurrentHP() {

        return currentLives;

    }

    private IEnumerator HitInvunerableCoroutine() {
        int layer = gameObject.layer;
        gameObject.layer = 0;

        yield return new WaitForSeconds(hitInvulnerableTime);

        gameObject.layer = layer;
    }

}
