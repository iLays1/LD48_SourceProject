using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
}
