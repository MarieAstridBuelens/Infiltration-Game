using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//script qui prend le transform du player. Chaque ennemi connaît
//la position du player quand il en aura besoin

public class AICharacterController : MonoBehaviour
{
    

    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    public Transform waypointsGroup;//on passe le transform parent de tous les waypoints

    [Tooltip("Put the parent Transform of all the waypoints you want the character to visit here")] public Transform currentWaypoint;

    public enum AIControllerState{
        Patrol,
        Pursue
    }

    public AIControllerState state;
    public static bool visiblePlayer = false;

    [SerializeField, Space(10), Header("Dev tools")] private bool debug;

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
        visiblePlayer = CheckPlayerVisibility();
        if (visiblePlayer){
            state = AIControllerState.Pursue;
        } else{
            state = AIControllerState.Patrol;
        }

        switch(state){
            case AIControllerState.Patrol:
                navMeshAgent.SetDestination(currentWaypoint.position);
                break;
            case AIControllerState.Pursue:
                navMeshAgent.SetDestination(playerTransform.position);
                break;
        }
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
        if (other.CompareTag("Player")){
            SceneManager.LoadScene("Defeat Screen");
        }
    }

    bool CheckPlayerVisibility(){
        RaycastHit hit;
        if (debug) Debug.DrawRay(transform.position, transform.forward * 10f);
        if(Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, 10f)){
            if(hit.collider.CompareTag("Player")){
                if(Vector3.Angle(transform.forward, playerTransform.position - transform.position) < 45){
                    if (debug) Debug.Log("Vu !");
                    return true;
                }
            }
        }
        return false;
    }
}
