using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobotNavAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player3D.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        AINavigation();
    }

    private void AINavigation() {
        agent.SetDestination(player.position);
        //transform.LookAt(player.position);
    }
}
