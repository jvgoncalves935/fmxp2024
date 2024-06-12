using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Dialogue001 : Dialogue3D
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private TriggerThirdPersonDialogue trigger;

    private void Start() {
        trigger = GetComponent<TriggerThirdPersonDialogue>();

        Init();
    }
    public override void Init() {
        dialogues = new List<DialogueData>();

        dialogues.Add(new DialogueData("PETSFARM_000", 1, "Hank", "Hey sparky!", 2.0f));
        dialogues.Add(new DialogueData("PETSFARM_001", 1, "Hank", "Anna, Bossy and Frank disappeared!", 2.5f));
        dialogues.Add(new DialogueData("PETSFARM_002", 0, "Sparky", "What happened, do you know anything?", 2.8f));
        dialogues.Add(new DialogueData("PETSFARM_003", 1, "Hank", "It's Sweetie, she got them!", 2.5f));
        dialogues.Add(new DialogueData("PETSFARM_004", 0, "Sparky", "Okay, so they didn't disappear, she captured them...", 3.2f));
        dialogues.Add(new DialogueData("PETSFARM_005", 1, "Hank", "What difference does it make? They are in danger!", 3.0f));
        dialogues.Add(new DialogueData("PETSFARM_006", 1, "Hank", "You need to save our friends!", 2.5f));
        dialogues.Add(new DialogueData("PETSFARM_007", 0, "Sparky", "Leave it to me!", 2.0f));
        
    }

    public override void Play() {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine() {
        for(int i = 0; i < dialogues.Count; i++) {
            SwitchCamera(cameras[dialogues[i].Camera]);
            trigger.SetDialogue(dialogues[i].Character,"\"" + dialogues[i].Text + "\"");
            yield return new WaitForSeconds(dialogues[i].Duration);
        }
        trigger.FinishDialogue();
    }

    private void SwitchCamera(CinemachineVirtualCamera virtualCamera) {
        if(CameraSwitcher.ActiveCamera != virtualCamera) {
            CameraSwitcher.SwitchCamera(virtualCamera);
        }
    }
}
