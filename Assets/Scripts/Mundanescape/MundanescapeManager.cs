using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundanescapeManager : MonoBehaviour
{
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

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
        CheckScenesDataInstanced();
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
}
