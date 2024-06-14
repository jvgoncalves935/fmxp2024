using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if(EnemiesUtils.IsEnemyBody(collider)) {
            Debug.Log(collider.name);
            collider.GetComponent<Enemy3D>().Kill();
        }
    }

}
