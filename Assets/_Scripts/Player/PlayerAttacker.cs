using System;
using System.Collections;
using UnityEngine;

public class PlayerAttacker : Player {

    public Action<Direction> OnAttack { get; private set; }

    [SerializeField] private LayerMask enemyMask;
    
    [SerializeField, Tooltip("How long should the attack trigger be enabled in physics steps.")] 
    private int attackDuration = 5;

	[SerializeField, Tooltip("How long should the delay between attacks be in seconds.")] 
    private float m_attackDelay = .5f;
	public float attackDelay => m_attackDelay;

    protected override void Awake() {
        base.Awake();

        OnAttack = Attack;

        foreach (Transform child in transform) child.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if ((1 << other.gameObject.layer | enemyMask) == enemyMask) {
            
            other.gameObject.GetComponent<EnemyScript>().TakeDamage(1); //Temporarily using 1

        }
    }

    private void Attack(Direction direction) => 
	StartCoroutine(AttackCoroutine(transform.Find("Attack" + direction.ToString()).gameObject));

    private IEnumerator AttackCoroutine(GameObject direction) {

        direction.SetActive(true);

        AudioManager.Instance.PlayOneShot("Slash");

        for (int i = 0; i < attackDuration; i++) yield return new WaitForFixedUpdate();

        direction.SetActive(false);
    }

}
