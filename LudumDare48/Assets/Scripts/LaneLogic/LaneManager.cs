using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager instance;
    public Lane[] lanes;

    private void Awake()
    {
        instance = this;
    }
}
