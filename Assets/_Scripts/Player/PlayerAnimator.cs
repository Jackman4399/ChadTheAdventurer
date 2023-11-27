using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : Player {

    private Animator animator;

    private PlayerAttacker attacker;
    
    private Vector2 lastMove = Vector2.right;

    [SerializeField] private string moveXName = "moveX", moveYName = "moveY",
    speedName = "speed", attackName = "attack";

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();

        attacker = transform.parent.GetComponentInChildren<PlayerAttacker>();
    }

	private void OnEnable() {
        userInput.Gameplay.Attack.performed += OnAttack;
    }

    private void OnDisable() {
        userInput.Gameplay.Attack.performed -= OnAttack;
    }

    protected override void Update() {
        base.Update();

        if (move != Vector2.zero && !Mathf.Approximately(Mathf.Abs(move.x), Mathf.Abs(move.y))) lastMove = move;

        animator.SetFloat(moveYName, lastMove.y);
        animator.SetFloat(moveXName, lastMove.x);
        animator.SetFloat(speedName, move.magnitude);
    }

    private void OnAttack(InputAction.CallbackContext context) => animator.SetTrigger(attackName);

	// Use from within the animation events
    private void AttackDirection(Direction direction) => attacker.OnAttack?.Invoke(direction);

}
