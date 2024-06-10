using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerVolume : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector3 boxSize;
    private void Awake() {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = boxSize;
    }

    void Start()
    {
        InitVolumeObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitVolumeObjects() {
        virtualCamera = transform.Find("Virtual Camera").gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }



    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            if(CameraSwitcher.ActiveCamera != virtualCamera) {
                CameraSwitcher.SwitchCamera(virtualCamera);
            }
        }
    }
}
