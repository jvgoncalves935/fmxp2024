using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemiesUtils
{
    public static void ToggleAttackCollider(EnemyAttackCollider3D attackCollider, bool toggle) {
        attackCollider.gameObject.SetActive(toggle);
    }

    public static bool IsEnemyBody(Collider collider) {
        if(collider.tag == "Enemy") {
            return true;
        }
        return false;
    }
}
