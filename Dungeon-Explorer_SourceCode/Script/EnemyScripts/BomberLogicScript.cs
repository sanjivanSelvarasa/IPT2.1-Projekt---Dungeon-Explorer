using System.Collections;
using UnityEngine;

public class BomberLogicScript : MonoBehaviour
{

    // Contains all Enemy Information
    public EnemiesData enemiesData;

    private Transform playerPos;
    private float RunDis;
    private float AttackDis;
    private Rigidbody2D rb;
    private float enemySpeed;

    private float disPlayerEnemy;

    public Animator anim;

    public EnemyHealthScript enemyHealthScript;
    public EnemyScript enemyScript;

    public float knockbackStrength = 10f;

    // Attack
    private float nextAttack;
    public GameObject bombObject;
    // Pathfinding
    public float radius = 5f;

    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        // set Health
        enemyHealthScript.SetMaxHealth(enemiesData.Health);

        RunDis = enemiesData.RunDis;
        AttackDis = enemiesData.AttackDis;
        enemySpeed = enemiesData.speed;
    }

    private void Update()
    {
        //EnemyAttackAnimation();
        EnemyMove();
        AttackPlayer();
    }

    private void EnemyMove()
    {
        disPlayerEnemy = Vector3.Distance(transform.position, playerPos.position);
        EnemyAnim();
    }

    private void EnemyAnim()
    {
        if (rb.linearVelocityX > 0.3)
        {
            if (rb.linearVelocityX > 0)
                transform.localScale = new Vector3(-0.7f, transform.localScale.y, 0.7f);
            else
                transform.localScale = new Vector3(0.7f, transform.localScale.y, 0.7f);
        }
    }

    private void AttackPlayer()
    {

        if (disPlayerEnemy <= AttackDis && Time.time >= nextAttack)
        {
            enemyScript.DamagePlayer(enemiesData.damageValue);
            nextAttack = Time.time + 1.0f;
        }

        if (disPlayerEnemy <= AttackDis && Time.time >= nextAttack)
        {
            Instantiate(bombObject, playerPos);


            nextAttack = Time.time + 1.0f;
        }
    }

    private IEnumerator Timer()
    {
        float tim1 = 0f;
        if (tim1 <= 3f)
        {
            tim1 += 1f *  Time.deltaTime;
            yield return null;
        }
    }

    public void EnemyTakeDamage(Vector3 dirKnock)
    {
        dirKnock += new Vector3(-dirKnock.x * knockbackStrength, -dirKnock.y + (knockbackStrength * 10), 0);
    }
}
