using System.Collections.Generic;
using UnityEngine;
using static PlayerVisionScript;

public class enemyControlScirpt : MonoBehaviour
{
    public EnemiesData enemiesData;

    // Timer
    private float updateTimer = 0f;
    private float updateInverval = 0.2f;

    private bool sightToPlayer = false;
    private bool sightToRadius = false;
    private Vector2 shortageCell;

    private float speed;
    public float enemyRadius = 10f;

    // ref to playerVision
    private PlayerVisionScript playerVision;
    private EnemyVisionManager enemyVision;

    void Start()
    {
        speed = enemiesData.speed;

        GameObject gameObject = GameObject.FindWithTag("Player");
        playerVision = gameObject.GetComponent<PlayerVisionScript>();

        GameObject gameObject2 = GameObject.FindWithTag("EnemyVisionManager");
        enemyVision = gameObject2.GetComponent<EnemyVisionManager>();
    }

    private void FixedUpdate()
    {
        EnemySummary(transform, enemyRadius);
    }

    public void EnemySummary(Transform t, float radius)
    {
        updateTimer += Time.fixedDeltaTime;
        if (updateTimer >= updateInverval)
        {
            updateTimer = 0f;
            sightToPlayer = EnemySightToPlayer(t);
            EnemySightRadius(t, radius);
        }

        MoveEnemy(t);
    }

    private void MoveEnemy(Transform t)
    {
        Vector2 dir = (playerVision.player.position - t.position).normalized;
        if (sightToPlayer)
        {
            t.position += (Vector3)dir * speed * Time.deltaTime;
        }
        else if (sightToRadius)
        {
            if (Mathf.Abs(Vector2.Distance(shortageCell, t.position)) > 0.1f)
            {
                Vector2 dirToCell = (shortageCell - (Vector2)t.position).normalized;
                t.position += (Vector3)(dirToCell * speed * Time.deltaTime);
            }
        }
    }

    private void EnemySightRadius(Transform t, float radius)
    {
        HashSet<Vector2> seen = new HashSet<Vector2>();

        foreach (KeyValuePair<Vector2, GridCells> kv in playerVision.grid)
        {
            Vector2 place = kv.Key;
            if (Vector2.Distance(transform.position, place) > enemyRadius)
                continue;

            Vector2 dir = -(place - (Vector2)transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(place, dir, enemyRadius);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                seen.Add(place);
            }
        }

        enemyVision.SetVisibleCellsFor(GetInstanceID(), seen);
    }


    private bool EnemySightToPlayer(Transform t)
    {
        Vector2 dir = (playerVision.player.position - t.position).normalized;
        Vector2 bothSide = Vector2.Perpendicular(dir);


        Vector2 startPoint = (Vector2)t.position + (dir * 0.8f);

        Vector2 startPointLeft = (Vector2)t.position + bothSide * 0.8f;
        Vector2 dirLeft = (playerVision.player.position - ((Vector3)startPointLeft)).normalized;
        Debug.DrawRay(startPointLeft, dirLeft * enemyRadius, Color.magenta);

        Vector2 startPointRight = (Vector2)t.position - bothSide * 0.8f;
        Vector2 dirRight = (playerVision.player.position - ((Vector3)startPointRight)).normalized;
        Debug.DrawRay(startPointRight, dirRight * enemyRadius, Color.magenta);

        RaycastHit2D hit = Physics2D.Raycast(startPoint, dir, enemyRadius);
        RaycastHit2D hitLeft = Physics2D.Raycast(startPointLeft, dirLeft, enemyRadius);
        RaycastHit2D hitRight = Physics2D.Raycast(startPointRight, dirRight, enemyRadius);
        Debug.DrawRay(startPoint, dir * enemyRadius, Color.blue);

        if (hit.collider != null && hitLeft.collider != null && hitRight.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
            else
            {
                sightToRadius = SightToRadius(t, dir);
            }
        }
        else
        {
            sightToRadius = SightToRadius(t, dir);
        }
        return false;
    }

    private bool SightToRadius(Transform t, Vector2 dirToPlayer)
    {
        List<Vector2> sight = new List<Vector2>();
        int id = GetInstanceID();

        HashSet<Vector2> seenByCurrEnemy = enemyVision.GetVisibleCellsFor(id);

        foreach (Vector2 cell in seenByCurrEnemy)
        {
            if (playerVision.grid.TryGetValue(cell, out GridCells gridcells))
            {
                if (gridcells.sightToPlayer)
                {
                    sight.Add(cell);
                }
            }
        }

        if (sight.Count == 0) return false;

        float shortageDis = float.MaxValue;
        Vector2 shortageCell = Vector2.zero;
        foreach (Vector2 sightCell in sight)
        {
            float dir = Vector2.Distance(sightCell, t.position);
            if (dir < shortageDis)
            {
                shortageDis = dir;
                shortageCell = sightCell;
            }
        }
        this.shortageCell = shortageCell;
        // Debug.Log(shortageCell);
        return true;
    }

    private void OnDrawGizmos()
    {
        if (enemyVision == null || playerVision == null)
            return;

        int id = gameObject.GetInstanceID();
        HashSet<Vector2> visCells = enemyVision.GetVisibleCellsFor(id);

        Gizmos.color = Color.blue;

        foreach (Vector2 pos in visCells)
        {
            if (playerVision.grid.ContainsKey(pos))
            {
                Gizmos.DrawWireCube(pos, Vector3.one * 0.9f);
            }
        }
    }
}
