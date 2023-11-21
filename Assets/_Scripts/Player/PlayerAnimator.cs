using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : Player {

    private Animator animator;

    private PlayerAttacker playerAttacker;
    
    private Vector2 lastMove = Vector2.right;

    [SerializeField] private string moveVerticalName = "moveVertical", moveHorizontalName = "moveHorizontal",
    speedName = "speed", attackName = "attack";

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();

        playerAttacker = transform.root.GetComponentInChildren<PlayerAttacker>();
    }

    protected override void Update() {
        base.Update();

        if (move != Vector2.zero && !Mathf.Approximately(Mathf.Abs(move.x), Mathf.Abs(move.y))) lastMove = move;

        animator.SetFloat(moveVerticalName, lastMove.y);
        animator.SetFloat(moveHorizontalName, lastMove.x);
        animator.SetFloat(speedName, move.magnitude);
    }

    protected override void Attack(InputAction.CallbackContext context) => animator.SetTrigger(attackName);

    private void OnAttack(int direction) => playerAttacker.OnAttack?.Invoke(direction);

}
