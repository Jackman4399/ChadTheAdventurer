using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }

public class NPCAnimator : MonoBehaviour {

    private Animator animator;

    [SerializeField] private Direction idleDirection;

	[SerializeField, Min(0)] private int NPCID;

	private string moveYName = "moveY", moveXName = "moveX";

    private void Awake() {
        animator = GetComponent<Animator>();

		animator.SetInteger("ID", NPCID);

        switch (idleDirection) {
            case Direction.Up:
				animator.SetFloat(moveYName, 1);
				animator.SetFloat(moveXName, 0);
            break;

            case Direction.Down:
				animator.SetFloat(moveYName, -1);
				animator.SetFloat(moveXName, 0);
            break;

            case Direction.Left:
				animator.SetFloat(moveYName, 0);
				animator.SetFloat(moveXName, -1);
            break;

            case Direction.Right:
				animator.SetFloat(moveYName, 0);
				animator.SetFloat(moveXName, 1);
            break;
        }
    }

}
