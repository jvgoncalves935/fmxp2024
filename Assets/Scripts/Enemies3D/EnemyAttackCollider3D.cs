using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider3D : MonoBehaviour
{
    private EnemyAttackCollider3D attackCollider;

    void Start()
    {
        InitColliderObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitColliderObjects() {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)){
            Debug.Log("Enemy damage");
        }
    }
}
