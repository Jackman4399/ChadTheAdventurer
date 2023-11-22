using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseScript : MonoBehaviour
{

     public Transform goal;

     public Animator animator;

     private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        

    }

    private void FixedUpdate() {

        // if(agent.velocity.x < 0) {

        // }

        animator.SetFloat("Speed", agent.velocity.magnitude);
        agent.destination = goal.position;

    }

}

