using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawsvilleMusicPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(PlayerUtils.IsPlayerBody(other)) {
            PawsvilleManager.Instance.PlayMusicWierd();
        }
    }
}
