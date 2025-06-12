using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceRandomDecoScript : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] decoTiles;
    
    public void PlaceDecoElements(HashSet<Vector2Int> gridFloor, int count)
    {
        tilemap.ClearAllTiles();
        Vector2Int[] gridFloorArray = gridFloor.ToArray();
        for (int i = 0; i <= count; i++)
        {
            int randIndex = Random.Range(0, gridFloor.Count);
            int randTile = Random.Range(0, decoTiles.Length);
            tilemap.SetTile((Vector3Int)gridFloorArray[randIndex], decoTiles[randTile]);
        }
    }
}
