using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerChangeHealthBarIcon : MonoBehaviour
{
    [SerializeField] private Sprite sprite;

    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        InitTriggerObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) {
        if(!triggered && PlayerUtils.IsPlayerBody(collider)) {
            ChangeImage();
        }
    }

    private void ChangeImage() {
        HealthBarPlayer3D.Instance.ChangeSprite(sprite);
    }

    private void InitTriggerObjects() {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
