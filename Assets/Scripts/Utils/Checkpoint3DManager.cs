using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint3DManager : MonoBehaviour
{
    [SerializeField] private Checkpoint3D currentCheckpoint;
    [SerializeField] private Vector3 currentCheckpointPosition;

    public static GameObject instance;
    private static Checkpoint3DManager _instance;
    public static Checkpoint3DManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<Checkpoint3DManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<Checkpoint3DManager>().gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Checkpoint3D GetCurrentCheckpoint() {
        return currentCheckpoint;
    }

    public void SetCurrentCheckpoint(Checkpoint3D checkpoint) {
        currentCheckpoint = checkpoint;

        float x = currentCheckpoint.transform.position.x;
        float y = currentCheckpoint.transform.position.y;
        float z = currentCheckpoint.transform.position.z;

        currentCheckpointPosition = new Vector3(x, y, z);
    }
}
