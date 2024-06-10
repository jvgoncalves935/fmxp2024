using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturescapeManager: MonoBehaviour
{
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

    public static GameObject instance;
    private static FuturescapeManager _instance;
    public static FuturescapeManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<FuturescapeManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<FuturescapeManager>().gameObject;
    }

    private void Start() {
        CrashGameStackOverflow();
        CheckScenesDataInstanced();
    }
    // Update is called once per frame
    void Update() {

    }

    private void CrashGameStackOverflow() {
        StartCoroutine(CrashGameStackOverflowCoroutine());
    }

    private IEnumerator CrashGameStackOverflowCoroutine() {
        yield return new WaitForSeconds(5.0f);

        JavascriptAlert.Call();
        
        SaveData saveData = SaveSystem.CarregarData();
        Debug.Log("Game not crashed.");
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
