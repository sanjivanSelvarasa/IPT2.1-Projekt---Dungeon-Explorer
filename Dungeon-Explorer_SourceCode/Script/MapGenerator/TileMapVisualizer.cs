using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, WallTilemap;

    [SerializeField]private RuleTile ruleWalls;

    [SerializeField]private Tile[] floorTile;

    [SerializeField]private GameObject spawnPoint;

    public void PlaceSpawnPoint(Vector2Int pos)
    {
        GameObject spawnPoint = Instantiate(this.spawnPoint);
        spawnPoint.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) 
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable positions, Tilemap tilemap, TileBase[] tiles)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tiles, (Vector2Int)position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase[] tiles, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        int randTile = UnityEngine.Random.Range(0, tiles.Length);
        tilemap.SetTile(tilePos, tiles[randTile]);
    }

    public void Clear()
    {
        if(floorTilemap != null)
            floorTilemap.ClearAllTiles();
        WallTilemap.ClearAllTiles();
    }

    public void DetectWallsAndPaint(IEnumerable<Vector2Int> floorPos)
    {
        var wallPos = FindWalls(floorPos);
        PaintWalls(wallPos);
    }

    private void PaintWallTile(Vector2Int position)
    {
        Vector3Int tilePos = (Vector3Int)position;
        WallTilemap.SetTile(tilePos, ruleWalls);
    }

    public void PaintWalls(IEnumerable<Vector2Int> wallPos)
    {
        foreach (Vector2Int pos in wallPos)
        {
            PaintWallTile(pos);
        }
    }

    public static HashSet<Vector2Int> FindWalls(IEnumerable<Vector2Int> floorPos)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();

        foreach (Vector2Int pos in floorPos)
        {
            foreach(Vector2Int dir in Direction2D.cardDirection)
            {
                Vector2Int neighbour = pos + dir;
                if (!floorPos.Contains(neighbour))
                    wallPos.Add(neighbour);
            }
        }
        return wallPos;
    }

    private static class Direction2D
    {
        public static List<Vector2Int> cardDirection = new List<Vector2Int>()
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };
    }
}
