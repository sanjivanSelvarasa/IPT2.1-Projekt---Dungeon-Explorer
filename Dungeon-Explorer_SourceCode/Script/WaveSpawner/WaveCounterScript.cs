using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveCounterScript : MonoBehaviour
{
    public TextMeshProUGUI textCounter;

    private void Start()
    {
        textCounter = GetComponent<TextMeshProUGUI>();
    }
    public void IncreaseWave(int nextWave)
    {
        textCounter.text = Convert.ToString(nextWave);
    }
}
