using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/************************************************
 * class responsible for moving the iceSphere towards the target
 * 
 * component of IceSphere
 * 
 * Pacifica Morrow
 * 01.14.2025
 ***********************************************/

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;
    private Rigidbody targetRb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        targetRb = target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }
}
