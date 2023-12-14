using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimator : EnemyAnimator { 

    private Vector2 lastMove;
    [SerializeField] private Direction idleFaceDirection;

    [SerializeField] private string
    moveXName = "moveX", moveYName = "moveY";

    private bool isAttacking;

    protected override void Awake() {
        base.Awake();

        switch (idleFaceDirection) {
            case Direction.Down: lastMove = Vector2.down; break;
            case Direction.Up: lastMove = Vector2.up; break;
            case Direction.Right: lastMove = Vector2.right; break;
            case Direction.Left: lastMove = Vector2.left; break;
        }
    }

    protected override void Update() {
        base.Update();

        if (moveDirection.sqrMagnitude > .1f) lastMove = moveDirection;

        animator.SetFloat(moveXName, lastMove.x);
        animator.SetFloat(moveYName, lastMove.y);

        if (agent.remainingDistance > agent.stoppingDistance && isAttacking) return;

        
    }

}
