using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = slider.maxValue;
    }

    public void Add(float amount)
    {
        slider.value = Mathf.Clamp(slider.value + amount, 0, slider.maxValue);
    }

    public void Sub(float amount)
    {
        slider.value = Mathf.Clamp(slider.value - amount, 0, slider.maxValue);
    }

    public bool IsEmpty() => slider.value <= 0;
}
