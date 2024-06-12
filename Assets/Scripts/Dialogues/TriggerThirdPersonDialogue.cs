using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;

public class TriggerThirdPersonDialogue : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraPlayer;
    [SerializeField] private bool interactButtonPressed = false;
    
    private Dialogue3D dialogue;
    private PlayerControls3D playerControls;
    private Vector3 initialPositionPlayer;
    private Quaternion initialRotationPlayer;
    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    private TMP_Text characterNameText;
    private TMP_Text textArea;
    private GameObject dialogueObj;


    // Start is called before the first frame update
    void Start()
    {
        InitTriggerObjects();
    }

    // Update is called once per frame
    void Update()
    {
        OnInteract(playerControls.Player.Interact);
    }

    private void InitTriggerObjects() {
        GetComponent<MeshRenderer>().enabled = false;
        Transform initialPositionPlayerTransform = transform.Find("InitialPositionPlayer");
        initialPositionPlayerTransform.GetComponent<MeshRenderer>().enabled = false;

        initialPositionPlayer = initialPositionPlayerTransform.position;
        initialRotationPlayer = initialPositionPlayerTransform.rotation;

        playerControls = new PlayerControls3D();
        playerControls.Enable();

        dialogue = GetComponent<Dialogue3D>();

        dialogueObj = transform.Find("DialoguesTextArea").gameObject;
        characterNameText = transform.Find("DialoguesTextArea/CharacterNameText").GetComponent<TMP_Text>();
        textArea = transform.Find("DialoguesTextArea/TextArea").GetComponent<TMP_Text>();

    }

    private void OnTriggerStay(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            if(interactButtonPressed) {
                InitDialogue();
            }
        }
    }

    private void InitDialogue() {
        if(dialogueStarted) {
            return;
        }

        dialogueStarted = true;
        SetNewPlayerPosition();
        Player3D.Instance.TogglePlayerMovement(false);
        SetDialogue("", "");
        dialogue.Play();

    }

    private void OnInteract(InputAction interact) {
        interactButtonPressed = interact.IsPressed();
    }

    private void SetNewPlayerPosition() {
        Player3D.Instance.transform.position = initialPositionPlayer;
        Player3D.Instance.transform.rotation = initialRotationPlayer;
    }

    public void FinishDialogue() {
        dialogueFinished = true;
        SetDialogue("", "");

        CameraSwitcher.SwitchCamera(cameraPlayer);
        Player3D.Instance.TogglePlayerMovement(true);
        gameObject.SetActive(false);
    }

    public void SetDialogue(string characterName, string text) {
        characterNameText.text = characterName;
        textArea.text = text;
    }

    private void ToggleDialog() {

    }
}
