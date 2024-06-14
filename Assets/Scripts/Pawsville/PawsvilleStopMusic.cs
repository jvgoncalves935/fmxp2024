using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawsvilleStopMusic : MonoBehaviour
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
            PawsvilleManager.Instance.StopMusicPeople();
            PawsvilleManager.Instance.StopMusicWierd();
        }
    }
}
