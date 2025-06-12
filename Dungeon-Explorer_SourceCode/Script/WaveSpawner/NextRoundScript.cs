using System.Threading;
using UnityEngine;

public class NextRoundScript : MonoBehaviour
{
    public EnemyCounterScript enemyBarScript;
    public SimpleRandomWalkGenerator randomWalkGen;
    public Transform playerTransform;

    private bool playerNearSpawner;
    public float cooldownMapGenTimer = 4f;
    private float timer = 4f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = cooldownMapGenTimer;
        }
    }
    private void FixedUpdate()
    {
        playerNearSpawner = CheckPlayerIsNearSpawner();
        // NextRoundBegin();
    }

    public bool NextRoundBegin()
    {
        if (playerNearSpawner)
        {
            randomWalkGen.RunProceduralGeneration();
            return true;
        }
        //Debug.Log("Is in NextRound method");
        return false;
    }

    private bool CheckPlayerIsNearSpawner()
    {
        if (Mathf.Clamp(playerTransform.position.x, -3f, 3f) == playerTransform.position.x && Mathf.Clamp(playerTransform.position.y, -2.5f, 2.5f) == playerTransform.position.y)
        {
            return true;
        }
        return false;
    }
}
