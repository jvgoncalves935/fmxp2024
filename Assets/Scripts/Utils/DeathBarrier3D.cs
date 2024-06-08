using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier3D : MonoBehaviour
{
    void Start()
    {
        DisableMeshRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            RestartCurrentCheckpoint();
        }
    }

    private void DisableMeshRenderer() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void RestartCurrentCheckpoint() {
        Player3D.Instance.SetPlayerPositionCurrentCheckpoint();
    }
}
