using TMPro;
using UnityEngine;

public class NumOfUpgrades : MonoBehaviour
{
    public XPScript _XPScript;
    public TextMeshProUGUI numOfUpgrades;

    private void FixedUpdate()
    {
        numOfUpgrades.text = $"{_XPScript.currUpgrades}";
    }
}
