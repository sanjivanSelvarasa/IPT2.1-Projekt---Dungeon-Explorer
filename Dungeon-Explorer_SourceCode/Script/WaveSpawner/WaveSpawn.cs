using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public string SpawnName;
    public int numOfEnemies;
    public GameObject[] EnemyTypes;
    public float WaveInterval = 2f;
}

public class WaveSpawn : MonoBehaviour
{
    public Waves[] waves = new Waves[100];

    private Waves currentWave;
    private int currentWaveNumber;

    public GameObject[] enemiesTypes;
    // Spawn Script
    public EnemySpawn enemySpawn;

    // List of Valid Positions on Map
    List<Vector3> AllPositions = new List<Vector3>();

    // Time between Waves
    float nextWaveCooldown;

    // is Wave completed
    private bool waveComplete = false;
    private int currEntity;

    // Wave Counter
    public WaveCounterScript waveCounterScript;

    // Loading Bar
    public EnemyCounterScript healthBarScript;

    // NextRound Script
    public NextRoundScript nextRound;

    public SimpleRandomWalkGenerator randomWalkGen;

    // Grid generation for Enemy AI
    public PlayerVisionScript visionScript;

    // PowerUp Spawn Script
    public SpawnPowerUps spawnPowerUps;
    public int powerUpCounter = 12;
    public PowerUpCollectScript powerUpCollectScript;

    private void Start()
    {
        CreateAllWaves();
    }
    private void Update()
    {
        currEntity = enemySpawn.currEntity;
        currentWave = waves[currentWaveNumber];
        if (!waveComplete && nextRound.NextRoundBegin()) {
            randomWalkGen.RunProceduralGeneration();
            visionScript.CreateGrid();
            waveComplete = SpawnWave();
            spawnPowerUps.SpawnHealthPowerUp(AllPositions, powerUpCounter);
            currentWaveNumber++;
            waveCounterScript.IncreaseWave(currentWaveNumber);
            nextWaveCooldown = Time.time + (currentWave.WaveInterval * 1000);
        }
        else if(waveComplete && currEntity < 5)
        {
            //healthBarScript.StartLoading(currentWave.WaveInterval, 100);
            nextWaveCooldown -= Time.time;
            if (Time.time >= nextWaveCooldown) 
            { 
                
                waveComplete = false;
            }
        }
    }

    private void CreateAllWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].EnemyTypes = enemiesTypes;
            waves[i].SpawnName = $"Wave {i+1}";
            int rand = 0;

            if(i <= 10)
                rand = Random.Range(6, 25);
            else if (i <= 20)
                rand = Random.Range(14, 34);
            else if (i <= 30)
                rand = Random.Range(17, 39);
            else if (i <= 40)
                rand = Random.Range(19, 43);
            else if (i <= 50)
                rand = Random.Range(24, 50);
            else if (i <= 60)
                rand = Random.Range(28, 56);
            else if (i <= 70)
                rand = Random.Range(32, 60);
            else if (i <= 80)
                rand = Random.Range(35, 65);
            else if (i <= 90)
                rand = Random.Range(38, 68);
            else if (i <= 100)
                rand = Random.Range(42, 77);
            else if (i <= 110)
                rand = Random.Range(47, 80);
            waves[i].numOfEnemies = rand;
        }
    }

    public bool SpawnWave()
    {
        powerUpCollectScript.DetroyAllEnemies();
        enemySpawn.DetroyAllEnemies();
        //AllPositions.Clear();
        AllPositions = enemySpawn.GetSpawnPoint();
        return enemySpawn.SpawnEnemy(currentWave.numOfEnemies, AllPositions, currentWave.EnemyTypes);
    }
}
