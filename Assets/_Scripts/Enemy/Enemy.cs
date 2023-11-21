using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Call from other scripts to hurt enemy.
    public void TakeDamage(int damage) {
        currentHealth -= damage;

        //Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0) {

            Die();

        }

    }

    void Die() {
        //Play die animation
        animator.SetBool("IsDead", true);

        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
