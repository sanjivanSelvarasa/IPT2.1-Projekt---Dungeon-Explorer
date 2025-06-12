using System;
using TMPro;
using UnityEngine;

public class SetScoreScript : MonoBehaviour
{
    private TextMeshProUGUI textHighScore;
    public HighScoreScript _HighScoreScript;
    void Start()
    {
        textHighScore = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        int currHighScore = Convert.ToInt16(_HighScoreScript.GetHighScoreText());
        string finalNum = "";
        if (currHighScore < 9)
        {
             finalNum = "000000000" + currHighScore.ToString();
        }else if (currHighScore < 99)
        {
             finalNum = "00000000" + currHighScore.ToString();
        }
        else if (currHighScore < 999)
        {
             finalNum = "0000000" + currHighScore.ToString();
        }
        else if (currHighScore < 9999)
        {
             finalNum = "000000" + currHighScore.ToString();
        }
        else if (currHighScore < 99999)
        {
             finalNum = "00000" + currHighScore.ToString();
        }
        else if (currHighScore < 999999)
        {
             finalNum = "0000" + currHighScore.ToString();
        }
        else if (currHighScore < 9999999)
        {
            finalNum = "000" + currHighScore.ToString();
        }
        else if (currHighScore < 99999999)
        {
            finalNum = "00" + currHighScore.ToString();
        }
        else if (currHighScore < 999999999)
        {
            finalNum = "0" + currHighScore.ToString();
        }
        textHighScore.text = finalNum;
    }
}
