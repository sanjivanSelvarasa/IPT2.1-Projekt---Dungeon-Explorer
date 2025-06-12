using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerVisionScript : MonoBehaviour
{
    [SerializeField] public bool PathVisual = true;

    // Advanced Grid Generation
    public Tilemap tilemap;
    private BoundsInt bounds;
    public Dictionary<Vector2, GridCells> grid;

    public bool pathGenerated = false;

    // player sight
    public float radius = 10f;
    public Transform player;

    // Timer
    private float updateTimer = 0f;
    private float updateInverval = 0.2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void FixedUpdate()
    {
        updateTimer += Time.fixedDeltaTime;
        if (updateTimer >= updateInverval)
        {
            updateTimer = 0f;
            PlayerSightRadius();
        }
    }
    public void CreateGrid()
    {
        bounds = tilemap.cellBounds;
        grid = new Dictionary<Vector2, GridCells>();

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (!tilemap.HasTile(tilePos))
                    continue;

                Vector3 worldPos = tilemap.CellToWorld(tilePos) + tilemap.cellSize / 2;
                grid.Add(worldPos, new GridCells(worldPos));
            }
        }
        pathGenerated = true;
    }

    private void PlayerSightRadius()
    {
        foreach (Vector2 place in grid.Keys)
        {
            //if (Vector2.Distance(place, player.position) > radius)
            //  continue;

            float dis = Vector2.Distance(place, player.position);
            if (dis <= radius)
            {
                Vector2 dir = -(place - (Vector2)player.position).normalized;
                RaycastHit2D raycast = Physics2D.Raycast(place, dir);

                if (raycast.collider != null)
                {
                    if (raycast.collider.tag == "Player")
                    {
                        grid[place].sightToPlayer = true;

                    }
                    else
                        grid[place].sightToPlayer = false;
                }
                else
                    grid[place].sightToPlayer = false;
                //Debug.DrawRay(place, dir * radius, Color.red);
            }
            else
            {
                grid[place].sightToPlayer = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!PathVisual || grid == null)
        {
            return;
        }

        if (grid != null)
        {
            foreach (GridCells cell in grid.Values)
            {
                Gizmos.color = Color.red;
                if (cell.sightToPlayer == true)
                    Gizmos.color = Color.gray;
                if (cell.enemyVision == true)
                    Gizmos.color = Color.white;
                Gizmos.DrawWireCube(cell.position, Vector3.one * 0.9f);
            }
        }
    }

    public class GridCells
    {
        public Vector2 position;

        public Vector2 connection;
        public bool isWall;
        public bool sightToPlayer;
        public bool enemyVision;

        public GridCells(Vector2 pos)
        {
            this.position = pos;
        }
    }
}
