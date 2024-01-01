using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossIdle : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float meleeRange = 3f;
    Transform player;
    Rigidbody2D rb;

    private LookAtPlayer look;

    private NavMeshAgent agent;
    

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
        // Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        agent.SetDestination(target);


        if (Vector2.Distance(player.position, rb.position) <= meleeRange) {
            //Attack melee
            animator.SetTrigger("MAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //animator.ResetTrigger("MAttack");
       
    }
}
