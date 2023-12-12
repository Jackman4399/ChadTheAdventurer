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
    protected Vector2 lastMove;
    [SerializeField] private Direction idleFaceDirection;

    [SerializeField] private string moveXName = "moveX", moveYName = "moveY", speedName = "speed", diedName = "died";

    protected EnemyHealth health;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch (idleFaceDirection) {
            case Direction.Down: lastMove = Vector2.down; break;
            case Direction.Up: lastMove = Vector2.up; break;
            case Direction.Right: lastMove = Vector2.right; break;
            case Direction.Left: lastMove = Vector2.left; break;
        }

        AssignAgent(GetComponentInParent<NavMeshAgent>);
        health = transform.parent.GetComponentInChildren<EnemyHealth>();
    }

    private void OnEnable() {
        health.OnHit += OnHit;
    }

    private void OnDisable() {
        health.OnHit -= OnHit;
    }

    protected virtual void Update() {
        moveDirection = agent.velocity;
        Debug.Log(moveDirection);

        if (moveDirection.sqrMagnitude > .1f) lastMove = moveDirection;

        animator.SetFloat(moveXName, lastMove.x);
        animator.SetFloat(moveYName, lastMove.y);
        animator.SetFloat(speedName, moveDirection.sqrMagnitude);
    }

    private void OnHit(int lives, Vector2 _) {
        StopAllCoroutines();
        StartCoroutine(HitAnimation());
        if (lives == 0) animator.SetTrigger(diedName);
    }

    private IEnumerator HitAnimation() {
        float hitBlinkTime = .5f;

        Coroutine blinkAnimation = StartCoroutine(BlinkAnimation());

        yield return new WaitForSeconds(hitBlinkTime);

        StopCoroutine(blinkAnimation);

        spriteRenderer.color = Color.white;
    }

    private IEnumerator BlinkAnimation() {
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
