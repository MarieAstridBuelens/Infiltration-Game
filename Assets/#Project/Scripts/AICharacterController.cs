using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

//script qui prend le transform du player. Chaque ennemi connaît
//la position du player quand il en aura besoin

public class AICharacterController : MonoBehaviour
{
    
    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    public Transform waypointsGroup;//on passe le transform parent de tous les waypoints

    private Transform currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentWaypoint = SelectDestination();
        navMeshAgent.SetDestination(currentWaypoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        //navMeshAgent.SetDestination(playerTransform.position);
    }

    Transform SelectDestination(){
        int index = Random.Range(0, waypointsGroup.childCount);
        Transform newwaypoint = waypointsGroup.GetChild(index);

        while (newwaypoint == currentWaypoint){
            index = Random.Range(0, waypointsGroup.childCount);
            newwaypoint = waypointsGroup.GetChild(index);
        }

        return newwaypoint;
    }

    void OnTriggerEnter(Collider other){
        if(other.transform == currentWaypoint){
            currentWaypoint = SelectDestination();
            navMeshAgent.SetDestination(currentWaypoint.position);
        }
    }

    bool CheckPlayerVisibility(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, 10f)){
            if(hit.collider.CompareTag("Player")){
                if(Vector3.Angle(transform.forward, playerTransform.position - transform.position) < 45){
                    return true;
                }
            }
        }
        return false;
    }
}
