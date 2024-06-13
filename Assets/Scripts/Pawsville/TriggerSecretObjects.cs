using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSecretObjects : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectSecret;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectSecret.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        gameObjectSecret.SetActive(true);
    }
}
