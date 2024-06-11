using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public IEnumerator FadeInImageCoroutine(Image image, float duration) {
        float time;
        image.color = new Color(1, 1, 1, 0);
        for(time = 0.0f;time <= duration; time += Time.deltaTime) {
            image.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), time / duration);
            yield return null;
        }
        image.color = new Color(1, 1, 1, 1);
    }

    public IEnumerator FadeOutImageCoroutine(Image image, float duration) {
        float time;
        image.color = new Color(1, 1, 1, 1);
        for(time = duration;time >= 0.0f;time -= Time.deltaTime) {
            image.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), time / duration);
            yield return null;
        }
        image.color = new Color(1, 1, 1, 0);
    }
}
