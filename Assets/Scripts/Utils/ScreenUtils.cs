using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUtils: MonoBehaviour{
    public static GameObject instance;
    private static ScreenUtils _instance;
    public static ScreenUtils Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<ScreenUtils>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<ScreenUtils>().gameObject;
    }
    public void FadeInImage(Image image, float duration) {
        StartCoroutine(FadeInImageCoroutine(image, duration));
    }

    public void FadeOutImage(Image image, float duration) {
        StartCoroutine(FadeOutImageCoroutine(image, duration));
    }

    public void FadeInText(TMP_Text text, float duration) {
        StartCoroutine(FadeInTextCoroutine(text, duration));
    }

    public void FadeOutText(TMP_Text text, float duration) {
        StartCoroutine(FadeOutTextCoroutine(text, duration));
    }

    private IEnumerator FadeInImageCoroutine(Image image, float duration) {
        float time;
        image.color = new Color(1, 1, 1, 0);
        for(time = 0.0f;time <= duration; time += Time.deltaTime) {
            image.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), time / duration);
            yield return null;
        }
        image.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator FadeOutImageCoroutine(Image image, float duration) {
        float time;
        image.color = new Color(1, 1, 1, 1);
        for(time = duration;time >= 0.0f;time -= Time.deltaTime) {
            image.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), time / duration);
            yield return null;
        }
        image.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator FadeInTextCoroutine(TMP_Text text, float duration) {
        float time;
        text.color = new Color(1, 1, 1, 0);
        for(time = 0.0f;time <= duration;time += Time.deltaTime) {
            text.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), time / duration);
            yield return null;
        }
        text.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator FadeOutTextCoroutine(TMP_Text text, float duration) {
        float time;
        text.color = new Color(1, 1, 1, 1);
        for(time = duration;time >= 0.0f;time -= Time.deltaTime) {
            text.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), time / duration);
            yield return null;
        }
        text.color = new Color(1, 1, 1, 0);
    }
}
