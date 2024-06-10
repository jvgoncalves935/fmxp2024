using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer3D : MonoBehaviour
{
    public static GameObject instance;
    private static HealthBarPlayer3D _instance;
    public static HealthBarPlayer3D Instance {
        get {
            if(_instance == null) {
                _instance = instance.GetComponent<HealthBarPlayer3D>();
            }
            return _instance;
        }
    }
    void Awake() {
        instance = FindObjectOfType<HealthBarPlayer3D>().gameObject;
    }

    
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    private Slider slider;
    private Image imageIcon;
    private RectTransform imageRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        InitHealthBar();
        InitBarObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitHealthBar()
    {
        slider = GetComponent<Slider>();

        int maxValue = Player3D.Instance.GetPlayerMaxHealth();
        slider.minValue = 0;
        slider.maxValue = maxValue;

        fill.color = gradient.Evaluate(1f);

        SetHealth(maxValue);
    }

    private void InitBarObjects() {
        imageIcon = transform.Find("Bar/Face").GetComponent<Image>();
        imageRectTransform = imageIcon.GetComponent<RectTransform>();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    public void ChangeSprite(Sprite sprite, bool shouldChangeSize, Vector2 size) {
        imageIcon.sprite = sprite;
        if(shouldChangeSize) {
            imageRectTransform.sizeDelta = size;
        }
    }
}
