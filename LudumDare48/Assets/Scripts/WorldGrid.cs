using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid main;

    Grid grid;

    public int width;
    public int height;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] TileData topLayerData;

    public WorldTile[,] tiles;

    private void Awake()
    {
        main = this;

        grid = GetComponent<Grid>();
        grid.cellSize = tilePrefab.transform.localScale;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new WorldTile[width,height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var go = Instantiate(tilePrefab, this.transform);
                go.transform.position = grid.CellToLocal(new Vector3Int(x,-y,0));
                var tile = go.GetComponent<WorldTile>();
                tiles[x, y] = tile;
                tile.coords = new Vector2Int(x,y);

                if (y == 0)
                    tile.SetData(topLayerData);
            }
        }

        transform.position -= new Vector3(width/2 * tilePrefab.transform.localScale.x ,0,0);
    }
}
