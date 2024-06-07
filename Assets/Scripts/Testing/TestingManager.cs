using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private AudioManager audioManager;

    public static GameObject instance;
    private static TestingManager _instance;
    public static TestingManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<TestingManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<TestingManager>().gameObject;
    }
    void Start()
    {
        CheckSceneLoaderInstanced();
    }

    public void CheckSceneLoaderInstanced() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            Instantiate(scenesData);
        }
    }
}
