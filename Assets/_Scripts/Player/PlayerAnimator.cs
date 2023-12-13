using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : Player {

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private Vector2 lastMove = Vector2.right;

    [Header("Animator Settings")]
    [SerializeField] private string speedName = "speed";
    [SerializeField] private string
	moveXName = "moveX", moveYName = "moveY",
	attackXName = "attackX", attackYName = "attackY",
    attackName = "attack", diedName = "died", hurtName = "hurt";

    [Header("Hit Invulnerable Settings")]
    [SerializeField] private Color flashColour = new Color(1, 0, 0, .5f);
    [SerializeField, Tooltip("Delay between flashes in seconds.")] 
    private float flashDelay = .5f;

    private PlayerAttacker attacker;
    private PlayerHealth health;

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        attacker = transform.parent.GetComponentInChildren<PlayerAttacker>();
        health = transform.parent.GetComponentInChildren<PlayerHealth>();
    }

	private void OnEnable() {
		attacker.OnAttack += OnAttack;
        health.OnHit += OnHit;
    }

    private void OnDisable() {
		attacker.OnAttack -= OnAttack;
        health.OnHit -= OnHit;
    }

    protected override void Update() {
        base.Update();

        if (move != Vector2.zero && !Mathf.Approximately(Mathf.Abs(move.x), Mathf.Abs(move.y))) lastMove = move;

		animator.SetFloat(moveXName, lastMove.x);
        animator.SetFloat(moveYName, lastMove.y);
        animator.SetFloat(speedName, move.sqrMagnitude);
    }

	private void OnAttack(Direction direction) {
        Vector2 attackDirection = Vector2.zero;

        switch (direction) {
            case Direction.Up: attackDirection = Vector2.up; break;
            case Direction.Down: attackDirection = Vector2.down; break;
            case Direction.Left: attackDirection = Vector2.left; break;
            case Direction.Right: attackDirection = Vector2.right; break;
        }

		animator.SetFloat(attackXName, attackDirection.x);
		animator.SetFloat(attackYName, attackDirection.y);

		animator.SetTrigger(attackName);
	}

	// Use from within the animation events
    private void AttackDirection(Direction direction) => attacker.Attack(direction);

    private void OnHit() {
        animator.SetTrigger(hurtName);
        StartCoroutine(HitInvulnerableCoroutine());
    }

    private IEnumerator HitInvulnerableCoroutine() {
        Coroutine flashCoroutine = StartCoroutine(FlashCoroutine());

        yield return new WaitForSeconds(health.HitInvulnerableTime);

        StopCoroutine(flashCoroutine);
    }

    private IEnumerator FlashCoroutine() {
        while (true) {
            spriteRenderer.color = flashColour;
            yield return new WaitForSeconds(flashDelay);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }
    }

}
