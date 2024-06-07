using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutsceneManager:MonoBehaviour
{
    public static GameObject instance;
    private static FinalCutsceneManager _instance;
    public static FinalCutsceneManager Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<FinalCutsceneManager>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<FinalCutsceneManager>().gameObject;
    }

    private void Start() {
        CrashGameStackOverflow();
    }
    // Update is called once per frame
    void Update() {

    }

    private void CrashGameStackOverflow() {
        StartCoroutine(CrashGameStackOverflowCoroutine());
    }

    private IEnumerator CrashGameStackOverflowCoroutine() {
        yield return new WaitForSeconds(5.0f);

        #if UNITY_WEBGL
            JavascriptAlert.Call();
        #endif
        SaveData saveData = SaveSystem.CarregarData();
        Debug.Log("Game not crashed.");
    }
}
