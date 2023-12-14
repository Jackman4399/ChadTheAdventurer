using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAnimator : Enemy {

    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    protected Vector2 moveDirection;

    [SerializeField] protected string
    speedName = "speed", diedName = "died";

    private EnemyHealth health;

    protected virtual void Awake() {
        AssignAgent(GetComponentInParent<NavMeshAgent>);
        
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        health = transform.parent.GetComponentInChildren<EnemyHealth>();
    }

    private void OnEnable() {
        health.OnHit += OnHit;
    }

    private void OnDisable() {
        health.OnHit -= OnHit;
    }

    protected virtual void Update() {
        moveDirection = agent.desiredVelocity;

        animator.SetFloat(speedName, moveDirection.sqrMagnitude);
    }

    private void OnHit(int currentLives, Vector2 _) {
        StopAllCoroutines();
        StartCoroutine(HitCoroutine());
        if (currentLives == 0) animator.SetTrigger(diedName);
    }

    private IEnumerator HitCoroutine() {
        float hitBlinkTime = .5f;

        Coroutine blinkCoroutine = StartCoroutine(BlinkCoroutine());

        yield return new WaitForSeconds(hitBlinkTime);

        StopCoroutine(blinkCoroutine);

        spriteRenderer.color = Color.white;
    }

    private IEnumerator BlinkCoroutine() {
        float visiblityTime = .075f;

        while (true) {
            spriteRenderer.color = Color.black;
            yield return new WaitForSeconds(visiblityTime);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(visiblityTime);
        }
    }

    // For use within animation events
    private void DisableEnemy() {
        transform.parent.gameObject.SetActive(false);
    }
}
