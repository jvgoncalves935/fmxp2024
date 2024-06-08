using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundanescapeManager : MonoBehaviour
{
    public static GameObject instance;
    private static MundanescapeManager _instance;
    public static MundanescapeManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<MundanescapeManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<MundanescapeManager>().gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
