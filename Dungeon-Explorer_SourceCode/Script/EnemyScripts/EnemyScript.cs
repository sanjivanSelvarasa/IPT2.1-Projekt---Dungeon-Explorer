using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform playerTransform;
    private EnemySpawn enemySpawn;
    private float DespawnDis = 100;

    private EnemySpawn script;

    // For Damage Player
    private HealthScript healthScript;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

        GameObject enemySpawn = GameObject.Find("EnemySpawn");
        script = enemySpawn.GetComponent<EnemySpawn>();

        GameObject HealthScript = GameObject.Find("Health");
        healthScript = HealthScript.GetComponent<HealthScript>();
    }
    private void DespawnEnemy()
    {
        float distPlayerAEnemy = Vector2.Distance(playerTransform.position, transform.position);
        if (distPlayerAEnemy > DespawnDis)
        {
            Destroy(gameObject);
            script.currEntity--;
        }
    }

    public void DamagePlayer(int damageValue) {

        healthScript.DecreaseHealth(damageValue);
    }
}
