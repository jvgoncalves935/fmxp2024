using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollider3D : MonoBehaviour
{
    private EnemyAttackCollider3D attackCollider;
    private Enemy3D enemy;

    void Start()
    {
        InitColliderObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitColliderObjects() {
        enemy = transform.parent.GetComponent<Enemy3D>();
    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)){
            enemy.DamagePlayer();
        }
    }

    public void RegisterAttack() {
        
    }

}
