using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Dialogue002: Dialogue3D
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private TriggerThirdPersonDialogue trigger;

    private void Start() {
        trigger = GetComponent<TriggerThirdPersonDialogue>();

        Init();
    }
    public override void Init() {
        dialogues = new List<DialogueData>();

        dialogues.Add(new DialogueData("PAWSVILLE_000", 1, "Sunny", "Hey Sparky!", 2.0f));
        dialogues.Add(new DialogueData("PAWSVILLE_001", 0, "Sparky", "Hi Sunny! Unfortunately I can't talk right now, I need to save my friends, they're in danger!", 5.3f));
        dialogues.Add(new DialogueData("PAWSVILLE_002", 1, "Sunny", "Oh, you mean those bitches that abandoned you?", 3.8f));
        dialogues.Add(new DialogueData("PAWSVILLE_003", 0, "Sparky", "I need to save them!", 2.5f));
        dialogues.Add(new DialogueData("PAWSVILLE_004", 1, "Sunny", "They'll never come back.", 3.5f));
        dialogues.Add(new DialogueData("PAWSVILLE_005", 0, "Sparky", "Wait, what?", 2.5f));
        dialogues.Add(new DialogueData("PAWSVILLE_006", 1, "Sunny", "bark bark", 2.0f));

    }

    public override void Play() {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine() {
        for(int i = 0;i < dialogues.Count;i++) {
            SwitchCamera(cameras[dialogues[i].Camera]);
            trigger.SetDialogue(dialogues[i].Character, "\"" + dialogues[i].Text + "\"");
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
