using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunSparkyText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string text;
    private TMP_Text textBox;
    private RectTransform rectTransform;
    private ScreenUtils screenUtils;

    void Start()
    {
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        textBox = transform.GetChild(0).GetComponent<TMP_Text>();
        screenUtils = GetComponent<ScreenUtils>();
        textBox.text = text;

        IncreaseTextWidth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseTextWidth() {
        StartCoroutine(IncreaseTextWidthCoroutine());
    }

    private IEnumerator IncreaseTextWidthCoroutine() {
        screenUtils.FadeInText(textBox, 1.0f);
        for(int i = 0;i < 80;i++) {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + 1, rectTransform.sizeDelta.y);
            yield return new WaitForSeconds(0.025f);
        }
        screenUtils.FadeOutText(textBox, 1.0f);
        for(int i = 0;i < 80;i++) {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + 1, rectTransform.sizeDelta.y);
            yield return new WaitForSeconds(0.025f);
        }
        gameObject.SetActive(false);
    }
}
