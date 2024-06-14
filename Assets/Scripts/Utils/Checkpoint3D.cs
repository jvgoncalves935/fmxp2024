using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint3D : MonoBehaviour
{
    [SerializeField] private Vector3 checkpointPosition;
    private Quaternion checkpointRotation;
    private bool checkpointTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        DisableMeshRenderer();
        SetCheckpointPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCheckpointPosition() {
        checkpointPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        checkpointRotation = transform.rotation;
    }

    private void DisableMeshRenderer() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public Vector3 GetCheckpointPosition() {
        return checkpointPosition;
    }

    public Quaternion GetCheckpointRotation() {
        return checkpointRotation;
    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider) && !checkpointTriggered) {
            TriggerCheckpoint();
        }
    }

    private void TriggerCheckpoint() {
        Checkpoint3DManager.Instance.SetCurrentCheckpoint(this);
        checkpointTriggered = true;
    }
}
