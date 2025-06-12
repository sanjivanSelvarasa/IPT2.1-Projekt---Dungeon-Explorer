using System.Collections;
using UnityEngine;

public class EnemyLogicScript01 : MonoBehaviour
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
        EnemyAttackAnimation();
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
        anim.SetBool("IsWalkSide", false);
        anim.SetBool("IsWalkDown", false);
        anim.SetBool("IsWalkUp", false);

        if (rb.linearVelocityX > 0.3)
        {
            anim.SetBool("IsWalkSide", true);
            if (rb.linearVelocityX > 0)
                transform.localScale = new Vector3(-1, transform.localScale.y, 1);
            else
                transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
        else if (rb.linearVelocityY > 0.3)
        {
            anim.SetBool("IsWalkUp", true);
        }
        else if (rb.linearVelocityY < -0.3)
        {
            anim.SetBool("IsWalkDown", true);
        }
    }

    private void EnemyAttackAnimation()
    {
        if (disPlayerEnemy <= AttackDis)
        {
            anim.SetBool("IsNearEnough", true);
        }
        else
            anim.SetBool("IsNearEnough", false);
    }

    private void AttackPlayer()
    {

        if (disPlayerEnemy <= AttackDis && Time.time >= nextAttack)
        {
            enemyScript.DamagePlayer(enemiesData.damageValue);
            nextAttack = Time.time + 1.0f;
        }
    }

    public void EnemyTakeDamage(Vector3 dirKnock)
    {
        dirKnock += new Vector3(-dirKnock.x * knockbackStrength, -dirKnock.y + (knockbackStrength * 10), 0);
    }

    
}
