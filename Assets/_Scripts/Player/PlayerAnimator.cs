using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimator : Player {

    public event Action<Direction> OnAttack;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private Vector2 lastMove = Vector2.right;

    private InputState gameplayInputState;

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
		attacker.OnAttackPressed += OnAttackPressed;
        health.OnHit += OnHit;
        health.OnDied += OnDied;
    }

    private void OnDisable() {
		attacker.OnAttackPressed -= OnAttackPressed;
        health.OnHit -= OnHit;
        health.OnDied -= OnDied;
    }

    protected override void Update() {
        base.Update();

        if (move != Vector2.zero && !Mathf.Approximately(Mathf.Abs(move.x), Mathf.Abs(move.y))) lastMove = move;

		animator.SetFloat(moveXName, lastMove.x);
        animator.SetFloat(moveYName, lastMove.y);
        animator.SetFloat(speedName, move.sqrMagnitude);
    }

	private void OnAttackPressed(Direction direction) {
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

    private void OnHit(int _, Vector2 direction) {
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

    private void OnDied() {
        animator.SetTrigger(diedName);
    }

    // For use from within the animation events
    private void AttackDirection(Direction direction) => OnAttack?.Invoke(direction);

    // For use from within the animation events
    private void EnableGameplay() {
        InputManager.Instance.ChangeInput(gameplayInputState);
    }

    // For use from within the animation events
    private void DisableGameplay() {
        gameplayInputState = InputManager.Instance.CurrentInputState;
        InputManager.Instance.ChangeInput(InputState.None);
    }

    // For use from within the animation events
    private void DisablePlayer() {
        transform.parent.gameObject.SetActive(false);
    }

}
