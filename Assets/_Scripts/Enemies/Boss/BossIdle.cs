using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;

public class BossIdle : StateMachineBehaviour
{
    public float normalSpeed = 2.5f; // Normal speed for phase 1
    public float slowSpeed = 0.5f; // Slow speed for phase 2
    public float meleeRange = 3f;
    public float rangedRange = 1f;
    Transform player;
    Rigidbody2D rb;
    private LookAtPlayer look;
    private NavMeshAgent agent; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        look = animator.GetComponent<LookAtPlayer>();
        player = look.player;
        rb = animator.GetComponent<Rigidbody2D>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = normalSpeed;
        agent.isStopped = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player == null) {
            Debug.Log("Player not found!");
            return;
        }

        look.FacePlayer();
        agent.SetDestination(player.position);

        // Stop moving when attacking
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MAttack") || 
        animator.GetCurrentAnimatorStateInfo(0).IsName("RAttack") || 
        animator.GetCurrentAnimatorStateInfo(0).IsName("Laser")) {
            agent.isStopped = true;
        } else {
            agent.isStopped = false;
        }

        if (!animator.GetBool("BuffedState")) {
            agent.speed = normalSpeed; // Set speed to normal in phase 1
            if (UnityEngine.Random.value < 0.5f) { // Randomly pick between melee and ranged attack
                if(Vector2.Distance(player.position, rb.position) <= meleeRange){
                    animator.SetTrigger("MAttack");
                }
            } else {
                animator.SetTrigger("RAttack");
            }
        } else {
            agent.speed = slowSpeed; // Set speed to slow in phase 2
            if (UnityEngine.Random.value < 0.5f) { // Randomly pick between ranged and laser attack
                animator.SetTrigger("RAttack");
            } else {
                animator.SetTrigger("Laser");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("MAttack");
        animator.ResetTrigger("RAttack");
        animator.ResetTrigger("Laser");
        agent.isStopped = true;
    }
}

