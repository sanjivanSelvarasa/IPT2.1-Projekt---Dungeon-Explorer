using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkGenerator : MonoBehaviour
{
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;
    [SerializeField] private int iterations = 10;
    [SerializeField] private int walkLength = 10;
    [SerializeField] private bool startRandomlyEachIteration = true;

    [SerializeField]
    private TileMapVisualizer tileMapVisualizer;

    public PlaceRandomDecoScript _PlaceRandomDecoScript;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk();
        tileMapVisualizer.Clear();
        tileMapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizer);
        _PlaceRandomDecoScript.PlaceDecoElements(floorPos, 40);
        SpawnPoint();
    }

    private void SpawnPoint()
    {
        tileMapVisualizer.PlaceSpawnPoint(startPos);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for(int i = 0; i < iterations; i++)
        {
            var path = Procedural.SimpleRandomWalk(currentPos, walkLength);
            floorPos.UnionWith(path);
            if (startRandomlyEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }
        return floorPos;
    }
}
