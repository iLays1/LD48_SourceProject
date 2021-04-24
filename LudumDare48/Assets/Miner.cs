using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    bool hovered = false;
    bool grabbed = false;
    Camera cam;
    Collider2D col;

    WorldTile currentTile;

    private void Awake()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        var g = WorldGrid.main;
        SetTile(g.tiles[g.width / 2, 0]);
    }

    private void Update()
    {
        if (hovered && Input.GetMouseButtonDown(0))
        {
            grabbed = true;
            col.enabled = false;
        }
        if (grabbed && Input.GetMouseButtonUp(0))
        {
            grabbed = false;
            col.enabled = true;

            if(WorldTile.hoveredTile != null)
            {
                WorldTile.hoveredTile.OnMinerDrop(this);
            }
        }

        if(grabbed)
        {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void OnMouseEnter()
    {
        hovered = true;
    }
    private void OnMouseExit()
    {
        hovered = false;
    }

    public void SetTile(WorldTile tile)
    {
        currentTile = tile;

        var tpos = tile.transform.position.x;
        var tscale = tile.transform.localScale.x;
        var tleft = tpos - (tscale / 2);
        var tright = tpos + (tscale / 2);
        var posX = transform.position.x;
        float x = (posX > tleft && posX < tright) ? posX : Random.Range(tleft, tright);
        float y = tile.transform.position.y - ((tile.transform.localScale.y / 2) - (this.transform.localScale.y / 2));
        float z = -10;

        this.transform.position = new Vector3(x, y, z);
    }
    public void ReturnMiner()
    {
        SetTile(currentTile);
    }
}
