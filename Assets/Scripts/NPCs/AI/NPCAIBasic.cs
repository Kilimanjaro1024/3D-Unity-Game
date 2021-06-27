using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState {Patrol, Idle, MovingToTarget, Combat}
public class NPCAIBasic : MonoBehaviour
{
    public Transform curDestination;
    public List<Transform> destinations = new List<Transform>();
    private NavMeshAgent agent;
    private int destNum;
    private AIState curState;

    public bool alerted;
    void Start () {
        destNum = 0;
        agent = GetComponent<NavMeshAgent>();
        curDestination = destinations[destNum];
        agent.destination = curDestination.position;
    }

    void Update(){
        StateMachine();
    }

    public void StateMachine(){
        switch(curState){
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Idle:
                break;
            case AIState.Combat:
                break;
            case AIState.MovingToTarget:
                break;
        }
    }

    public void Patrol(){
        if(agent.remainingDistance < 0.5f){
            if(destinations.Count  == 0){
                return;
            }
            agent.destination = destinations[destNum].position;
            destNum = (destNum + 1) % destinations.Count;
        }
    }
}
