using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : Player {

    private Animator animator;

    private PlayerAttacker attacker;
    
    private Vector2 lastMove = Vector2.right;

    [SerializeField] 
	private string 
	moveXName = "moveX", moveYName = "moveY",
	attackXName = "attackX", attackYName = "attackY",
    speedName = "speed", attackName = "attack";

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();

        attacker = transform.parent.GetComponentInChildren<PlayerAttacker>();
    }

	private void OnEnable() {
		InitialiseAttackListeners(true);
    }

    private void OnDisable() {
		InitialiseAttackListeners(false);
    }

    protected override void Update() {
        base.Update();

        if (move != Vector2.zero && !Mathf.Approximately(Mathf.Abs(move.x), Mathf.Abs(move.y))) lastMove = move;

		animator.SetFloat(moveXName, lastMove.x);
        animator.SetFloat(moveYName, lastMove.y);
        animator.SetFloat(speedName, move.magnitude);
    }

	private void OnAttackUp(InputAction.CallbackContext context) => OnAttack(Vector2.up);
	private void OnAttackDown(InputAction.CallbackContext context) => OnAttack(Vector2.down);
	private void OnAttackLeft(InputAction.CallbackContext context) => OnAttack(Vector2.left);
	private void OnAttackRight(InputAction.CallbackContext context) => OnAttack(Vector2.right);

	private void OnAttack(Vector2 direction) {
		animator.SetFloat(attackXName, direction.x);
		animator.SetFloat(attackYName, direction.y);

		StartCoroutine(OnAttackCoroutine());
	}

	private IEnumerator OnAttackCoroutine() {
		animator.SetTrigger(attackName);

		InitialiseAttackListeners(false);

		yield return new WaitForSeconds(attacker.attackDelay);

		InitialiseAttackListeners(true);
	}

	// Use from within the animation events
    private void AttackDirection(Direction direction) => attacker.OnAttack?.Invoke(direction);

	private void InitialiseAttackListeners(bool enabled) {
		if (enabled) {
			userInput.Gameplay.AttackUp.performed += OnAttackUp;
			userInput.Gameplay.AttackDown.performed += OnAttackDown;
			userInput.Gameplay.AttackLeft.performed += OnAttackLeft;
			userInput.Gameplay.AttackRight.performed += OnAttackRight;
		} else {
			userInput.Gameplay.AttackUp.performed -= OnAttackUp;
			userInput.Gameplay.AttackDown.performed -= OnAttackDown;
			userInput.Gameplay.AttackLeft.performed -= OnAttackLeft;
			userInput.Gameplay.AttackRight.performed -= OnAttackRight;
		}
	}

}
