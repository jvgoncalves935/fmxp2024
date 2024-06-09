using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUtils
{
    public static bool IsPlayerBody(Collider collider) {
        if(collider.tag == "Player") {
            return true;
        }
        return false;
    }

    public static bool IsPlayerBody(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            return true;
        }
        return false;
    }
}
