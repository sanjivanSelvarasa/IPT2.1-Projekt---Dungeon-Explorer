using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnPowerUps : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject healthBoxTile; // Replace gameobject with real Tile
    public void SpawnHealthPowerUp(List<Vector3> possiblePos, int counterOfSpawn)
    {
        HashSet<Vector3> randPosList = new HashSet<Vector3>();

        for (int i = 0; i <= counterOfSpawn; i++)
        {
            if(possiblePos[Random.Range(0, possiblePos.Count)] != null)
                randPosList.Add(possiblePos[Random.Range(0, possiblePos.Count)]);
        }

        foreach (Vector3 pos in randPosList)
        {
            Instantiate(healthBoxTile, pos, Quaternion.identity);
        }
    }
}
