using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : Player {

    public Action<int> OnAttack { get; private set; }

    [SerializeField] private LayerMask enemyMask;

    [SerializeField, Tooltip("How long should the attack trigger be enabled in milliseconds.")] 
    private float attackDuration = 10;

    protected override void Awake() {
        base.Awake();

        OnAttack = Attack;

        foreach (Transform child in transform) child.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == enemyMask.value) {
            
        }
    }

    private void Attack(int direction) => StartCoroutine(AttackCoroutine(direction));

    private IEnumerator AttackCoroutine(int direction) {
        transform.GetChild(direction).gameObject.SetActive(true);

        yield return new WaitForSeconds(attackDuration / 1000);

        transform.GetChild(direction).gameObject.SetActive(false);
    }

}
