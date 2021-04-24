using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public static WorldTile hoveredTile;
    public TileDataEvent OnSwitchState = new TileDataEvent();

    public TileData data;
    public TileData onClickTestState;

    public Vector2Int coords;

    private void Start()
    {
        SetData(data);
    }

    public void SetData(TileData newState)
    {
        data = newState;
        OnSwitchState.Invoke(newState);
    }

    public WorldTile[] GetOccupiableNeighbor()
    {
        var tc = coords;
        List<WorldTile> tiles = new List<WorldTile>();
        
        WorldTile tileRight;
        if (tc.x + 1 > WorldGrid.main.width) tileRight = WorldGrid.main.tiles[tc.x + 1, tc.y];
        else tileRight = null;

        WorldTile tileLeft;
        if (tc.x - 1 < -1) tileLeft = WorldGrid.main.tiles[tc.x - 1, tc.y];
        else tileLeft = null;

        WorldTile tileTop;
        if (tc.y - 1 > -1) tileTop = WorldGrid.main.tiles[tc.x, tc.y - 1];
        else tileTop = null;

        WorldTile tileBot;
        if (tc.y + 1 > WorldGrid.main.height) tileBot = WorldGrid.main.tiles[tc.x, tc.y + 1];
        else tileBot = null;

        if (tileLeft != null && tileLeft.data is TileData_Occupiable)
            tiles.Add(tileLeft);
        if (tileRight != null && tileRight.data is TileData_Occupiable)
            tiles.Add(tileRight);
        if (tileTop == null || tileTop != null && tileTop.data is TileData_Occupiable)
            tiles.Add(tileTop);
        if (tileBot == null || tileBot != null && tileBot.data is TileData_Occupiable)
            tiles.Add(tileBot);

        return tiles.ToArray();
    }

    public void OnMinerDrop(Miner miner)
    {
        data.OnMinerDropped(this, miner);
    }

    private void OnMouseEnter()
    {
        hoveredTile = this;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            SetData(onClickTestState);
    }
}
