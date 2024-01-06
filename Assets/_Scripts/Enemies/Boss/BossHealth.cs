using System;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : Enemy {

    public event Action<int, int> OnHit;
    public event Action OnDied;

    public bool isInvulnerable = false;

    private new Rigidbody2D rigidbody;
    
    //Animator
    private Animator animator;

    [SerializeField, Min(1)] private int maxLives = 50;
    [SerializeField] private int currentLives;

    private void Awake() {
        AssignAgent(GetComponentInParent<NavMeshAgent>);

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentLives = maxLives;
    }

    public void TakeDamage(Vector2 direction, int damage, float knockback) {

        if(isInvulnerable) return;

        currentLives -= damage;

        OnHit?.Invoke(currentLives, maxLives);

        if (currentLives > 0) rigidbody.AddForce(direction * knockback);

        if(currentLives <= 0) {

            agent.isStopped = true;
            animator.SetBool("IsDead", true);

            StoryManager.Instance.MakeChoice(ChoiceState.BossChoice, 1);
            StoryManager.Instance.Proceed();

            MenuManager.Instance.ChangeMenu(MenuState.Win);
            InputManager.Instance.ChangeInput(InputState.Menu);

            OnDied?.Invoke();

        } else if(((float) currentLives)/maxLives <= 0.5f) {

            animator.SetBool("BuffedState", true);

        }
    }

}
