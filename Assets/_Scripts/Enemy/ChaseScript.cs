using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseScript : MonoBehaviour
{

     public Transform goal;

     public Animator animator;

     public NavMeshAgent agent;

     private Transform player;

     public LayerMask playerMask;

    private void FixedUpdate() {

        // if(agent.velocity.x < 0) {

        // }

        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetFloat("Xaxis", agent.velocity.x);

        if(player != null) {
            
            agent.destination = player.position;

        } else if(goal != null){

            agent.destination = goal.position;
        }
            
        

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.layer.Equals("Player")) {
            Debug.Log("Player FOUND!");
            player = other.gameObject.transform;
        }

    }

}

