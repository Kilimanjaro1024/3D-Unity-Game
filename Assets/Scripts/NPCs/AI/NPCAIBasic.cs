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
    private float combatRange = 3f;
    public bool alerted = false;
    private MainHand mainHand;
    private bool inCombat = false;
    public float dist;

    void Awake(){
        mainHand = gameObject.GetComponentInChildren<MainHand>();
        mainHand.heldItem = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<ItemObject>();
    }
    void Start () {
        destNum = 0;
        agent = GetComponent<NavMeshAgent>();
        curDestination = destinations[destNum];
        agent.destination = curDestination.position;
    }

    void Update(){
        dist = agent.remainingDistance;
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
                Combat();
                break;
            case AIState.MovingToTarget:
                MoveToTarget();
                break;
        }
    }

    private void UpdateState(){
        if(alerted){
            // Debug.Log("Alerted");
            curState = AIState.MovingToTarget;
            if(agent.remainingDistance <= combatRange){
                curState = AIState.Combat;
            }
            // else{
            //     Debug.Log("OUT OF RANGE");
            //     // agent.destination = target.position;
            //     // curState = AIState.MovingToTarget;
            // }
        }
    }

    private void Patrol(){
        agent.stoppingDistance = 0;
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
        agent.stoppingDistance = 2.5f;
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

    private void Combat(){
        // Debug.Log("IN COMBAT");
        // Debug.Log(agent.remainingDistance);
        // Debug.Log(agent.destination);
        agent.destination = target.position;
        if(agent.remainingDistance > combatRange){
            curState = AIState.MovingToTarget;
        }
        StartCoroutine(CombatSequence());
    }

    IEnumerator CombatSequence(){
        // Debug.Log(agent.remainingDistance);
        if(agent.remainingDistance > combatRange){
            curState = AIState.MovingToTarget;
        }
        mainHand.QuickAttack();
        yield return new WaitForSeconds(3);
        mainHand.QuickAttack();
        StopAllCoroutines();
        
    }
}
