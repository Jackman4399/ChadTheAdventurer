using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    protected NavMeshAgent agent;

    protected void AssignAgent(Func<NavMeshAgent> dependency) {
        agent = dependency?.Invoke();

        agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

}
