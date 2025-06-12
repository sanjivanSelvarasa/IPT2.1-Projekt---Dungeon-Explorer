using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDir(floorPos, Direction2D.cardinalDirectionList);
        tileMapVisualizer.DetectWallsAndPaint(floorPos);
    }

    private static HashSet<Vector2Int> FindWallsInDir(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();

        foreach (var position in floorPos)
        {
            foreach (var direction in directionList)
            {
                var neighbourPos = position + direction;
                if (!floorPos.Contains(neighbourPos))
                {
                    wallPos.Add(neighbourPos);
                }
            }
        }
        return wallPos;
    }
}
