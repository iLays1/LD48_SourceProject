using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileDataEvent : UnityEvent<TileData> { }

public abstract class TileData : ScriptableObject
{
    public Color col;

    public abstract void OnMinerDropped(WorldTile tile, Miner miner);
}
