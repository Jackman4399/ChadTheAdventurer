using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacker : Enemy {

    [SerializeField] protected float pushbackForce = 1000;

    private bool canAttack;

    private EnemyController controller;

    protected virtual void Awake() {
        AssignAgent(GetComponentInParent<NavMeshAgent>);

        controller = GetComponentInParent<EnemyController>();

        canAttack = true;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!canAttack) return;
        if ((1 << other.gameObject.layer | controller.PlayerMask) != controller.PlayerMask) return;
        Vector2 direction = (other.transform.position - transform.position).normalized;
        other.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * direction);
    }

    public void DisableAttack() {
        canAttack = false;
    }

}
