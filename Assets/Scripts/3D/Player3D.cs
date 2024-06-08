using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour
{
    public static GameObject instance;
    private static Player3D _instance;
    public static Player3D Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<Player3D>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<Player3D>().gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerPositionCurrentCheckpoint() {
        Checkpoint3D currentCheckpoint = Checkpoint3DManager.Instance.GetCurrentCheckpoint();

        float x = currentCheckpoint.transform.position.x;
        float y = currentCheckpoint.transform.position.y;
        float z = currentCheckpoint.transform.position.z;

        Vector3 newPlayerPosition = new Vector3(x, y, z);

        ChangePlayerPosition(newPlayerPosition);
    }

    private void ChangePlayerPosition(Vector3 newPosition) {
        gameObject.transform.position = newPosition;
    }
}
