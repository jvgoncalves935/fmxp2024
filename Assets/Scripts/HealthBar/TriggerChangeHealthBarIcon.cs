using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerChangeHealthBarIcon : MonoBehaviour
{
    [SerializeField] private Sprite spriteIcon;
    [SerializeField] private bool shouldChangeSize;
    [SerializeField] private Vector2 size;
    [SerializeField] private Sprite spriteHealthBar;
    [SerializeField] private bool shouldChangeBar;

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
        HealthBarPlayer3D.Instance.ChangeSprite(spriteIcon,shouldChangeSize,size, shouldChangeBar, spriteHealthBar);
    }

    private void InitTriggerObjects() {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
