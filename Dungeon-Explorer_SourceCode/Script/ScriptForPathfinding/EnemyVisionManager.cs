using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionManager : MonoBehaviour
{
        public Dictionary<int, HashSet<Vector2>> enemyVisionMap = new Dictionary<int, HashSet<Vector2>>();

        public void SetVisibleCellsFor(int enemyId, HashSet<Vector2> cells)
        {
            enemyVisionMap[enemyId] = cells;
        }

        public HashSet<Vector2> GetVisibleCellsFor(int enemyId)
        {
            return enemyVisionMap.TryGetValue(enemyId, out var cells) ? cells : new HashSet<Vector2>();
        }
}
