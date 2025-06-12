using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Procedural { 
    
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int WalkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var previousPos = startPos;

        for (int i = 0; i < WalkLength; i++)
        {
            var newPos = previousPos + Direction2D.RandomDirection();
            path.Add(newPos);
            previousPos = newPos;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
        List<Vector2Int> corrider = new List<Vector2Int>();
        var direction = Direction2D.RandomDirection();
        var currentPos = startPos;

        for (int i = 0; i < corridorLength; i++)
        {
            currentPos += direction;
            corrider.Add(currentPos);
        }

        return corrider;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // UP
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0), // LEFT
        new Vector2Int(1, 0), // RIGHT
    };

    public static Vector2Int RandomDirection()
    {
        int choosePath = Random.Range(0, cardinalDirectionList.Count);
        return Direction2D.cardinalDirectionList[choosePath];
    }
}
