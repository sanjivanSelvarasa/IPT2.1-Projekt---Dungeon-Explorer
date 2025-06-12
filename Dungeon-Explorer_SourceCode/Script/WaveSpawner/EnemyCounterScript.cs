using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterScript : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }

    public float GetValue()
    {
        return slider.value;
    }

    public void StartLoading(float timeInSec, int maxValue)
    {
        StartCoroutine(LoadingToMaxSlider(timeInSec, maxValue));
    }

    private IEnumerator LoadingToMaxSlider(float timeInSec, int maxValue)
    {
        float delay = timeInSec / 100;
        for (int i = 0; i <= 100; i++)
        {
            SetValue(i);
            yield return new WaitForSeconds(delay);
        }
    }

    
}
