using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDynamic : Dialogue3D
{
    [SerializeField] private string character;
    [SerializeField] private string text;
    [SerializeField] private float duration;

    private TriggerThirdPersonDialogue trigger;

    private void Start() {
        trigger = GetComponent<TriggerThirdPersonDialogue>();

        Init();
    }
    public override void Init() {


    }

    public override void Play() {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine() {
        trigger.SetDialogue(character, "\"" + text + "\"");
        yield return new WaitForSeconds(duration);
        trigger.FinishDialogue();
    }

}
