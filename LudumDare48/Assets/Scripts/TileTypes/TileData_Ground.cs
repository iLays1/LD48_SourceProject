using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileData_Ground : TileData
{
    public override void OnMinerDropped(WorldTile tile, Miner miner)
    {
        miner.ReturnMiner();
    }
}
