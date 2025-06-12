using UnityEngine;

public class HighScoreScript : MonoBehaviour
{
    private int count;
    private void Start()
    {
        count = 0;
    }
    public string GetHighScoreText()
    {
        return PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    public void ChangeHighScore(int num) {
        if (num > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", num);
        }
    }

    public void IncreaseHighScore(int value)
    {
        count += value;
        if (count > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", count);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
