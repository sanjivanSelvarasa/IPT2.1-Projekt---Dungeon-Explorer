using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPScript : MonoBehaviour
{
    private Slider slider;
    private int currValue;
    private int minValue = 0;
    [SerializeField]private int maxValue = 100;

    // Upgrades
    public int currUpgrades;

    // Text Field Value
    private TextMeshProUGUI ValueTextField;

    void Start()
    {
        slider = GetComponent<Slider>();
        currValue = 0;
        slider.value = currValue;

        ValueTextField = GetComponentInChildren<TextMeshProUGUI>();

    }

    private void UpdateText()
    {
        ValueTextField.text = $"{Convert.ToString(Mathf.Clamp(currValue, minValue, maxValue))}/{Convert.ToString(maxValue)}";
    }
    public void IncreaseSliderValue(int value)
    {
        if (currValue < maxValue)
            slider.value = Mathf.Clamp(currValue += value, minValue, maxValue);
        if (slider.value == maxValue || slider.value > maxValue)
        {
            IncreaseUpgrades(1);
            ResetValue();
        }
        UpdateText();
    }

    public void ResetValue()
    {
        currValue = minValue;
        slider.value = currValue;
        UpdateText();
    }

    public void IncreaseUpgrades(int value)
    {
        currUpgrades += value;
        UpdateText();
    }

    public void DecreaseUpgrades(int value)
    {
        currUpgrades -= value;
        UpdateText();
    }
}
