using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment instance;
    public UnitAction[] actions;

    private void Awake()
    {
        instance = this;
    }
}
