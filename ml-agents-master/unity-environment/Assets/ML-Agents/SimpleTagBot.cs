using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleTagBot : MonoBehaviour {

    public GameObject[] Targets;
    public Transform CurrentTarget;
    private NavMeshAgent agent;
    public float Speed = 2;
    //public float Speed = 2;
    // Use this for initialization
    void Start () {
       // Targets = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTarget();
        agent.SetDestination(CurrentTarget.position);


    }
    void UpdateTarget()
    {
        float _distance;
        float ClosestDistance =500;

        foreach (GameObject MLAgent in Targets)
        {
            _distance = Vector3.Distance(transform.position, MLAgent.transform.position);
            if (_distance< ClosestDistance)
            {
                ClosestDistance = _distance;
                CurrentTarget = MLAgent.transform;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //-1
        
    }
}
