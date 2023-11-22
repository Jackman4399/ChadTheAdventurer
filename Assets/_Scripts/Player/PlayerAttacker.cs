using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : Player {

    public Action<int> OnAttack { get; private set; }

    [SerializeField] private LayerMask enemyMask;

    [SerializeField, Tooltip("How long should the attack trigger be enabled in frames.")] 
    private int attackDuration = 3;

    protected override void Awake() {
        base.Awake();

        OnAttack = Attack;

        foreach (Transform child in transform) child.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if ((int)Mathf.Pow(2, other.gameObject.layer) == enemyMask) {
            //TODO: damage enemy here
        }
    }

    private void Attack(int direction) => StartCoroutine(AttackCoroutine(direction));

    private IEnumerator AttackCoroutine(int direction) {
        transform.GetChild(direction).gameObject.SetActive(true);

        for (int i = 0; i < attackDuration; i++) yield return null;

        transform.GetChild(direction).gameObject.SetActive(false);
    }

}
