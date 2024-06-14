using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawsvilleManager : MonoBehaviour
{
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private AudioSource audioSourcePeople;
    [SerializeField] private AudioSource audioSourceWierd;

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
        CheckScenesDataInstanced();
        TravarCursor();
        PlayMusicPeople();
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
        Cursor.visible = false;
    }

    public static void DestravarCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayMusicPeople() {
        audioSourcePeople.Play();
    }

    public void StopMusicPeople() {
        audioSourcePeople.Stop();
    }

    public void PlayMusicWierd() {
        audioSourceWierd.Play();
    }

    public void StopMusicWierd() {
        audioSourceWierd.Stop();
    }
}
