using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MaraNav : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent navMeshMara;
    // Start is called before the first frame update
    void Start()
    {
        navMeshMara = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshMara.SetDestination(player.position);
    }
}
