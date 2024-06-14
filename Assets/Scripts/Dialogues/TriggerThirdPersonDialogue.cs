using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;

public class TriggerThirdPersonDialogue : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraPlayer;
    [SerializeField] private bool isDynamicTextOriented;
    [SerializeField] private bool isActivatedOnCollision;

    
    private bool interactButtonPressed = false;
    private Dialogue3D dialogue;
    private PlayerControls3D playerControls;
    private Vector3 initialPositionPlayer;
    private Quaternion initialRotationPlayer;
    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    private TMP_Text characterNameText;
    private TMP_Text textArea;
    private GameObject dialogueObj;

    private GameObject interactTextObj;
    private bool interactTextEnabled = false;


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
        Transform initialPositionPlayerTransform = transform.parent.transform.Find("InitialPositionPlayer");
        initialPositionPlayerTransform.GetComponent<MeshRenderer>().enabled = false;

        interactTextObj = transform.parent.transform.Find("InteractText").gameObject;
        ToggleInteractText(false);

        initialPositionPlayer = initialPositionPlayerTransform.position;
        initialRotationPlayer = initialPositionPlayerTransform.rotation;

        playerControls = new PlayerControls3D();
        playerControls.Enable();

        dialogue = GetComponent<Dialogue3D>();

        dialogueObj = transform.parent.transform.Find("DialoguesTextArea").gameObject;
        characterNameText = transform.parent.transform.Find("DialoguesTextArea/CharacterNameText").GetComponent<TMP_Text>();
        textArea = transform.parent.transform.Find("DialoguesTextArea/TextArea").GetComponent<TMP_Text>();

        ToggleDialog(false);
    }

    private void OnTriggerStay(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            if(interactButtonPressed) {
                InitDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            if(isDynamicTextOriented) {
                ToggleInteractText(true);
            } else {
                InitDialogue();
            }
        }      
    }

    private void OnTriggerExit(Collider collider) {
        if(PlayerUtils.IsPlayerBody(collider)) {
            ToggleInteractText(false);
        }
    }

    private void InitDialogue() {
        if(dialogueStarted) {
            return;
        }

        ToggleInteractText(false);
        dialogueStarted = true;


        if(!isActivatedOnCollision) {
            SetNewPlayerPosition();
        }
        
        Debug.Log("teste");

        if(!isDynamicTextOriented) {

            Player3D.Instance.TogglePlayerMovement(false);
            Player3D.Instance.ToggleAttackCollider(false);
            Player3D.Instance.AuthorizeAttack(false);
            //characterController.Move(Vector3.zero * Time.deltaTime);
        }

        ToggleDialog(true);
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
        ToggleDialog(false);

        CameraSwitcher.SwitchCamera(cameraPlayer);
        Player3D.Instance.TogglePlayerMovement(true);
        Player3D.Instance.ToggleAttackCollider(true);
        Player3D.Instance.AuthorizeAttack(true);
        gameObject.SetActive(false);
    }

    public void SetDialogue(string characterName, string text) {
        characterNameText.text = characterName;
        textArea.text = text;
    }

    private void ToggleDialog(bool toggle) {
        dialogueObj.SetActive(toggle);
    }

    private void ToggleInteractText(bool toggle) {
        interactTextObj.SetActive(toggle);
    }
}
