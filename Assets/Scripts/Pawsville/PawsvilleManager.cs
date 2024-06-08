using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawsvilleManager : MonoBehaviour
{
    public static GameObject instance;
    private static PawsvilleManager _instance;
    public static PawsvilleManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<PawsvilleManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<PawsvilleManager>().gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
