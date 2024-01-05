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

    public bool Heal(int lives) {
        // checks if it overheals, do nothing if it does
        if (currentLives + lives > maxLives) return false;

        //Heal the player
        currentLives += lives;

        // invoke current lives changed event
        OnLivesChanged?.Invoke(currentLives);

        return true;
    }

    public void TakeDamage(Vector2 direction, int damage) {
        if (invulnerable) return;

        //Hurt the player
        currentLives -= damage;
        OnLivesChanged?.Invoke(currentLives);

        //Prevent negative health
        if(currentLives == 0) {
            MenuManager.Instance.ChangeMenu(MenuState.Lose);
            InputManager.Instance.ChangeInput(InputState.Menu);

            if (StoryManager.Instance.CurrentStoryState == 
            StoryState.ParticipateEmergencyQuest) {
                StoryManager.Instance.MakeChoice(ChoiceState.BossChoice, 2);
                StoryManager.Instance.Proceed();
            }

            OnDied?.Invoke(); 
            invulnerable = true;
        } else {
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
