using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    // Spawning Entitys (Enemy)
    public Transform playerTransform;
    public GameObject enemieObject;
    public Transform enemieTransform;
    public float radius = 50f;
    private Vector3 posSpawn;

    // Entity numbers
    public int maxEntity = 100;
    public int currEntity = 0;

    // Spawn Area and get Spawn Points
    public Tilemap tilemap;

    // Set Value to ChargeBar
    private EnemyCounterScript healthBarScript;


    private void Start()
    {
        //GetSpawnPoint();

        GameObject healthBar = GameObject.Find("ValueBar");
        healthBarScript = healthBar.GetComponent<EnemyCounterScript>();
    }
    private void FixedUpdate()
    {
        // SpawnEnemy();
    }

    private void Update()
    {
        SetAmountInValueBar();
    }

    public List<Vector3> GetSpawnPoint()
    {
        List<Vector3> validPos = new List<Vector3>();
        BoundsInt boundsInt = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);

        for (int x=0; x < boundsInt.size.x; x++) {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    Vector3Int tilePos = new Vector3Int(boundsInt.xMin + x, boundsInt.yMin + y, 0);
                    Vector3 place = tilemap.GetCellCenterWorld(tilePos);
                    validPos.Add(place);
                    // Debug.Log(place);
                }
            }
        }

        // Debug.Log("Spawn Tiles: " + validPos.Count);
        return validPos;
    }

    public bool SpawnEnemy(int maxEntity, List<Vector3> validPos, GameObject[] enemieObject)
    {
        this.maxEntity = maxEntity;
        int currEntity = 0;
        this.currEntity = currEntity;
        bool isValidSpawn;
        while (currEntity < maxEntity)
        {
            isValidSpawn = false;

            float minPosPlayerX = playerTransform.position.x - radius;
            float maxPosPlayerX = playerTransform.position.x + radius;
            float minPosPlayerY = playerTransform.position.y - radius;
            float maxPosPlayerY = playerTransform.position.y + radius;
            //posSpawn = new Vector3(playerTransform.position.x + xPos, playerTransform.position.y + yPos, 0);
            //Instantiate(enemieObject, posSpawn, Quaternion.identity);
            int randSpawn = Random.Range(0, validPos.Count);
            if (validPos[randSpawn].x > minPosPlayerX && validPos[randSpawn].x < maxPosPlayerX) {
                if (validPos[randSpawn].y > minPosPlayerY && validPos[randSpawn].y < maxPosPlayerY)
                    isValidSpawn = true;
                else
                    isValidSpawn = false;
            }
            if (isValidSpawn) 
            {
                int randEnemy = Random.Range(0, enemieObject.Length);
                Instantiate(enemieObject[randEnemy], validPos[randSpawn], Quaternion.identity);
                currEntity++;
                this.currEntity = currEntity;
            }
            // Debug.Log(validPos[randSpawn]);
        }
        return true;
    }

    public void DetroyAllEnemies()
    {
        GameObject[] findAllEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in findAllEnemies)
        {
            Destroy(enemy);
        }

        currEntity = 0;
    }

    private void SetAmountInValueBar()
    {
        healthBarScript.SetMaxValue(maxEntity);
        healthBarScript.SetValue(currEntity);
    }
}
