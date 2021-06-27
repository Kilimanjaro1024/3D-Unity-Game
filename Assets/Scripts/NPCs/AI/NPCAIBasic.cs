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
    private float viewDistance = 10.0f;
    private RaycastHit hit;
    private bool rayHit;
    private Transform target;
    private float combatRange = 5f;
    public bool alerted = false;

    
    void Start () {
        destNum = 0;
        agent = GetComponent<NavMeshAgent>();
        curDestination = destinations[destNum];
        agent.destination = curDestination.position;
    }

    void Update(){
        UpdateState();
        StateMachine();
        FieldOfVision();
    }

    private void StateMachine(){
        switch(curState){
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Idle:
                break;
            case AIState.Combat:
                break;
            case AIState.MovingToTarget:
                MoveToTarget();
                break;
        }
    }

    private void UpdateState(){
        if(alerted){
            Debug.Log("Alerted");
            curState = AIState.MovingToTarget;
        }
    }

    private void Patrol(){
        if(agent.remainingDistance < 0.5f){
            if(destinations.Count  == 0){
                return;
            }
            agent.destination = destinations[destNum].position;
            destNum = (destNum + 1) % destinations.Count;
        }
    }
    
    private void MoveToTarget(){
        agent.destination = target.position;
        if(agent.remainingDistance <= combatRange){
            curState = AIState.Combat;
        }

    }

    private void FieldOfVision(){
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        rayHit = Physics.Raycast(gameObject.transform.position, forward, out hit, viewDistance);
        Debug.DrawRay(gameObject.transform.position, forward * viewDistance, Color.green);
        if(hit.transform != null && hit.transform.tag == "Player"){
            target = hit.transform;
            if(target != gameObject.transform){
                alerted = true;
            }
        }
    }
}
