using UnityEngine;

public class HitEnemyScript : MonoBehaviour
{
    public EnemyHealthScript enemyHealthScript;
    public int damageValue = 1;

    private void Start()
    {
       gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyHealthScript>(out EnemyHealthScript enemyHealth))
        {
            enemyHealth.EnemyDamage(damageValue);
        }
    }


}