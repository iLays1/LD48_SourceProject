using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileVisuals : MonoBehaviour
{
    Color baseColor;
    SpriteRenderer rend;
    WorldTile tile;

    private void Awake()
    {
        tile = GetComponent<WorldTile>();
        rend = GetComponent<SpriteRenderer>();
        baseColor = rend.color;

        tile.OnSwitchState.AddListener(OnSwitchStateEvent);
    }

    void OnSwitchStateEvent(TileData state)
    {
        baseColor = state.col;
        rend.color = baseColor;
    }

    private void OnMouseEnter()
    {
        rend.DOKill();
        rend.DOColor(baseColor * 0.5f, 0.1f);
    }
    private void OnMouseExit()
    {
        rend.DOKill();
        rend.DOColor(baseColor, 0.1f);
    }
}
