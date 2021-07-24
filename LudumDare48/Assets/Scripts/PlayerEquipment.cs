using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment instance;
    public UnitAction[] selectedActions;
    public UnitAction[] availableActions;

    private void Awake()
    {
        instance = this;
    }
}
