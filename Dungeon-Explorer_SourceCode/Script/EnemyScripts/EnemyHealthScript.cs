using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class EnemyHealthScript : MonoBehaviour
{
    // Contains all Enemies Information
    public EnemiesData enemiesData;

    // Enemy Health
    private int maxHealth;
    private int currentHealth = 0;
    public SpriteRenderer spriteRenderer;
    private EnemySpawn enemySpawnScript;
    public EnemyLogicScript01 enemyLogicScript01;
    private XPScript xpScript;
    private int XPValue;

    // Increase HighScore
    public HighScoreScript highScoreScript;

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject enemySpawn = GameObject.Find("EnemySpawn");
        enemySpawnScript = enemySpawn.GetComponent<EnemySpawn>();

        GameObject XPScript = GameObject.Find("Experience");
        xpScript = XPScript.GetComponent<XPScript>();

        GameObject HighScore = GameObject.Find("Highscore");
        highScoreScript = HighScore.GetComponent<HighScoreScript>();

        maxHealth = enemiesData.Health;
        XPValue = enemiesData.xpValue;

        // set Health at beginning
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
    }
    public void EnemyDamage(int value)
    {
        currentHealth -= value;
        StartCoroutine(ColorChange(Color.red, 0.5f));
        //enemyLogicScript01.EnemyTakeDamage(new Vector3(0,0,0));
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            enemySpawnScript.currEntity--;
            xpScript.IncreaseSliderValue(XPValue);
            highScoreScript.IncreaseHighScore(enemiesData.ScoreValue);
        }
    }

    public void IncreaseXPValue(int value)
    {
        XPValue += value;
    }
    private IEnumerator ColorChange(Color color, float duration)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }
}
