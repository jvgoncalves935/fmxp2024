using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsfarmManager : MonoBehaviour
{
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

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
        CheckScenesDataInstanced();
        TravarCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckScenesDataInstanced() {
        if(FindObjectOfType<ScenesData>() == null) {
            Instantiate(scenesData);
            scenesData = ScenesData.InstanciaScenesData;

            Instantiate(inputNames);
            inputNames = InputNames.InstanciaInputNames;

            Instantiate(sceneLoader);
            sceneLoader = SceneLoader.InstanciaSceneLoader;

            Instantiate(audioManager);
        }
    }

    public static void TravarCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public static void DestravarCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
