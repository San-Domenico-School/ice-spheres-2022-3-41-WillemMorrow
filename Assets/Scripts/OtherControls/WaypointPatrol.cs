using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/***********************************************
 * Sets the NavMeshAgent of an IceSphere to navigate around a set of waypoints.
 * 
 * component of IceSphere
 * 
 * Pacifica Morrow
 * 02.14.2025
 * ********************************************/

public class WaypointPatrol : MonoBehaviour
{
    private GameObject[] waypoints;
    private NavMeshAgent navMeshAgent;
    private int waypointIndex;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameManager.Singleton.waypoints;
        navMeshAgent = GetComponent<NavMeshAgent>();
        waypointIndex = Random.Range (0, waypoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[waypointIndex].transform.position);

        if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending)
        {
            waypointIndex++;
        }

        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
