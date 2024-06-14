using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FuturescapeManager: MonoBehaviour
{
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private InputNames inputNames;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private HealthBarPlayer3D healthBar;
    [SerializeField] private GameObject boxObject;

    [SerializeField] private AudioSource audio01;
    [SerializeField] private AudioSource audio02;
    [SerializeField] private AudioSource audio03;

    private int totalCoins = 89;
    [SerializeField] private EndingCutsceneDialog endingCutsceneDialog;
    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;

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
        healthBar.gameObject.SetActive(false);
        characterController =  Player3D.Instance.gameObject.GetComponent<CharacterController>();
        thirdPersonController = Player3D.Instance.gameObject.GetComponent<ThirdPersonController>();

        EndingCutscene();
        
        CheckScenesDataInstanced();

        TravarCursor();
    }
    // Update is called once per frame
    void Update() {

    }

    private void EndingCutscene() {
        StartCoroutine(EndingCutsceneCoroutine());
    }

    private IEnumerator EndingCutsceneCoroutine() {
        thirdPersonController.enabled = false;
        characterController.enabled = false;

        endingCutsceneDialog.InitImage(3.0f);
        yield return new WaitForSeconds(3.0f);

        yield return new WaitForSeconds(13.0f);

        endingCutsceneDialog.EndImage(5.0f);
        yield return new WaitForSeconds(5.0f);

        yield return new WaitForSeconds(3.1f);

        endingCutsceneDialog.DisableCutscene();
        thirdPersonController.enabled = true;
        characterController.enabled = true;
        CrashGameStackOverflow();

    }

    private void CrashGameStackOverflow() {
        StartCoroutine(CrashGameStackOverflowCoroutine());
    }

    private IEnumerator CrashGameStackOverflowCoroutine() {
        yield return new WaitForSeconds(0.1f);
        audio01.Play();
        yield return new WaitForSeconds(6.5f);

        boxObject.SetActive(false);
        audio01.Stop();
        audio02.Play();
        yield return new WaitForSeconds(4.803f);

        audio02.Stop();
        audio03.Play();


        healthBar.gameObject.SetActive(true);
        Time.timeScale = 0;
        thirdPersonController.enabled = false;
        characterController.enabled = false;

        yield return null;
        JavascriptAlert.Call();
        
        //SaveData saveData = SaveSystem.CarregarData();
        
        //Debug.Log("Game not crashed.");
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


}
