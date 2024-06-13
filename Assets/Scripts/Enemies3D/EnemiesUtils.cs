using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemiesUtils
{
    public static void ToggleAttackCollider(EnemyAttackCollider3D attackCollider, bool toggle) {
        Debug.Log(toggle);
        attackCollider.gameObject.SetActive(toggle);
    }

    public static bool IsEnemyBody(Collider collider) {
        if(collider.tag == "Enemy") {
            return true;
        }
        return false;
    }

    public static bool IsEnemyBody(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            return true;
        }
        return false;
    }
}
