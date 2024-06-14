using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobotNavAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private float alertRadius;
    private float patrolRadius;
    private Vector3 patrolRadiusCenter;
    private bool isPatrolling = false;
    private bool reachedRandomPoint;
    private Vector3 selectedRandomPointPatrol;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player3D.Instance.transform;

        EnemyRobotDog enemy = GetComponent<EnemyRobotDog>();
        alertRadius = enemy.GetAlertRadius();
        patrolRadiusCenter = enemy.GetPatrolRadiusPosition();
        patrolRadius = enemy.GetPatrolRadius();
    }

    // Update is called once per frame
    void Update()
    {
        AINavigation();
    }

    private void AINavigation() {
        if(PointDistance(player) <= alertRadius) {
            isPatrolling = false;
            agent.SetDestination(player.position);
        } else {
                     

            if(!isPatrolling) {
                isPatrolling = true;
                /*
                if(PointDistance(player) <= patrolRadius) {

                }
                if(RandomPoint(patrolRadiusCenter, patrolRadius, out selectedRandomPointPatrol)){
                    Debug.DrawRay(selectedRandomPointPatrol, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(selectedRandomPointPatrol);
                }
                */

            }

        }
    }

    private float PointDistance(Transform point) {
        return Vector3.Distance(transform.position, point.position);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
