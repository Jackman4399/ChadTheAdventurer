using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    protected NavMeshAgent agent;

    protected virtual void Awake() {
        agent = GetComponent<NavMeshAgent>();
        
        if (agent != null) {
            agent.updateRotation = false;
		    agent.updateUpAxis = false;
        }
    }

}
