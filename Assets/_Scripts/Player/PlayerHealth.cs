using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Player {

    public event Action<int> OnLivesChanged;
    public event Action<int, Vector2> OnHit;
    public event Action OnDied;

    [SerializeField] private int maxLives = 5;
    public int MaxLives => maxLives;
    //Purely for testing
    private int currentLives;
    
    [SerializeField, Tooltip("How long should the player be invulnerable once hit in seconds.")]
    private float hitInvulnerableTime = 2.5f;
    public float HitInvulnerableTime => hitInvulnerableTime;

    private bool invulnerable;
    public bool Invulnerable => invulnerable;

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

    public void TakeDamage(Vector2 direction) {
        if (invulnerable) return;

        //Hurt the player
        currentLives--;
        OnLivesChanged?.Invoke(currentLives);

        //Prevent negative health
        if(currentLives == 0) OnDied?.Invoke();
        else {
            OnHit?.Invoke(currentLives, direction);
            StartCoroutine(HitInvunerableCoroutine());
        }
        
    }

    private IEnumerator HitInvunerableCoroutine() {
        invulnerable = true;

        yield return new WaitForSeconds(hitInvulnerableTime);

        invulnerable = false;
    }

}
