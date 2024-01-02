using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;

public class BossIdle : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float meleeRange = 3f;

    public float rangedRange = 1f;
    Transform player;
    Rigidbody2D rb;

    private LookAtPlayer look;

    private NavMeshAgent agent;

    public bool isBuffed = false;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        look = animator.GetComponent<LookAtPlayer>();

        agent = animator.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.speed = speed;

        agent.isStopped = false;
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(player == null) {
            Debug.Log("Player not found!");
            return;
        }

        look.FacePlayer();
        Vector2 target = player.position;
        agent.SetDestination(target);

        if (!animator.GetBool("BuffedState")) {

            if (!animator.GetBool("Switch")) {

                if(Vector2.Distance(player.position, rb.position) <= meleeRange){

                    animator.SetTrigger("MAttack");
                    animator.SetBool("Switch", true);
                }
                

            } else if (animator.GetBool("Switch")) {
                agent.speed = 0;
                animator.SetTrigger("RAttack");
                animator.SetBool("Switch", false);
    
            }

        } else {

            if (!animator.GetBool("Switch2")) {
                
                animator.SetTrigger("RAttack");
                animator.SetBool("Switch2", true);

            } else if (animator.GetBool("Switch2")) {
                
                animator.SetTrigger("Laser");
                animator.SetBool("Switch2", false);

            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.ResetTrigger("MAttack");
        animator.ResetTrigger("RAttack");
        agent.isStopped = true;
    }
}
