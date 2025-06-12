using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    private Slider slider;
    private int currHealth;
    private int minHealth = 0;
    [SerializeField] private int maxHealth = 100;

    // Text Field Value
    private TextMeshProUGUI ValueTextField;

    void Start()
    {
        slider = GetComponent<Slider>();
        ValueTextField = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        UpdateText();
        Death();
    }

    private void UpdateText()
    {
        ValueTextField.text = $"{Convert.ToString(Mathf.Clamp(currHealth, minHealth, maxHealth))}/{Convert.ToString(maxHealth)}";
    }

    public void IncreaseHealth(int value)
    {
        slider.value = Mathf.Clamp(currHealth += value, minHealth, maxHealth);
        UpdateText();
    }

    public void DecreaseHealth(int value)
    {
        slider.value = Mathf.Clamp(currHealth -= value, minHealth, maxHealth);
        UpdateText();
    }

    public void SetHealth(int value)
    {
        currHealth = value;
        UpdateText();
    }

    public void SetMaxHealth()
    {
        currHealth = maxHealth;
        UpdateText();
    }

    public void SetMaxHealth(int value)
    {
        maxHealth += value;
        slider.maxValue = maxHealth;
        slider.value = Mathf.Clamp(currHealth, minHealth, maxHealth);
        UpdateText();
    }

    public void Death()
    {
        if (currHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public object GetMaxHealth()
    {
        return maxHealth;
    }
}
