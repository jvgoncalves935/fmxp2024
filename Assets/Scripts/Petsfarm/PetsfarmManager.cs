using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsfarmManager : MonoBehaviour
{
    public static GameObject instance;
    private static PetsfarmManager _instance;
    public static PetsfarmManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<PetsfarmManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<PetsfarmManager>().gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
