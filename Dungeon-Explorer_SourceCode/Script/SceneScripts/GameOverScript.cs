using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void GameOver()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        gameObject.SetActive(false);

    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
        gameObject.SetActive(false);
    }
}
