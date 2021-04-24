using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileData_Occupiable : TileData
{
    public override void OnMinerDropped(WorldTile tile, Miner miner)
    {
        if (tile.GetOccupiableNeighbor().Length > 0)
        {
            miner.SetTile(WorldTile.hoveredTile);
        }
        else
        {
            miner.ReturnMiner();
        }
    }
}
