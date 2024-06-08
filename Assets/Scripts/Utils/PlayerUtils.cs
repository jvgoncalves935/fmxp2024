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
}
