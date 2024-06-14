using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndingCutsceneDialog : MonoBehaviour
{
    [SerializeField] private Image image;
    private ScreenUtils screenUtils;
    // Start is called before the first frame update
    void Start()
    {
        screenUtils = GetComponent<ScreenUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitImage(float duration) {
        screenUtils.FadeInImage(image,duration);
    }

    public void EndImage(float duration) {
        screenUtils.FadeOutImage(image, duration);
    }

    public void DisableCutscene() {
        gameObject.SetActive(false);
    }


}
