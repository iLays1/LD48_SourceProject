using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerData : MonoBehaviour
{
    public static TestPlayerData instance;
    public UnitAction[] actions;

    private void Awake()
    {
        instance = this;
    }
}
