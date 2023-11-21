using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : Player {

    private Animator animator;
    
    private Vector2 lastMove;

    [SerializeField] private string moveVerticalName = "moveVertical", moveHorizontalName = "moveHorizontal",
    speedName = "speed";

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();

        if (move != Vector2.zero) lastMove = move;

        animator.SetFloat(moveVerticalName, lastMove.y);
        animator.SetFloat(moveHorizontalName, lastMove.x);
        animator.SetFloat(speedName, move.magnitude);
    }

}
