using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    private bool isInvincible = false;
    private Image fillImage;
    private Color originalColor;
    private bool isBooster = false;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = slider.maxValue;
        fillImage = slider.fillRect.GetComponent<Image>();
        originalColor = fillImage.color;
    }

    public void Add(float amount)
    {
        if (!isInvincible)
        {
            slider.value = Mathf.Clamp(slider.value + amount, 0, slider.maxValue);
        }
    }

    public void Sub(float amount)
    {
        if (!isInvincible)
        {
            slider.value = Mathf.Clamp(slider.value - amount, 0, slider.maxValue);
        }
    }

    public void SetInvincible(bool active)
    {
        isInvincible = active;
        fillImage.color = active ? new Color32(0, 255, 189, 255) : originalColor;
    }

    public void SetBooster(bool active)
    {
        isBooster = active;
        SetInvincible(active);
    }

    public bool IsEmpty() => slider.value <= 0;

    public bool IsInvincible() => isInvincible;

    public bool IsBooster() => isBooster;
}
