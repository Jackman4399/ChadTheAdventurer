using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public UnityEvent OnHitWithReference, OnDeathWithReference;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Call from other scripts to hurt enemy.
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        //Play hurt animation
        //animator.SetTrigger("Hurt");

        if(currentHealth <= 0) {

            OnDeathWithReference?.Invoke();
            Die();
            
        } else {

            OnHitWithReference?.Invoke();

        }

    }

    void Die() {
        //Play die animation
        //animator.SetBool("IsDead", true);

        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Destroy(this.gameObject);
    }

}
