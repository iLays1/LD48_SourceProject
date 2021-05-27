using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShieldModifier : UnitModifier
{
    public float shieldFactor;

    public override void Apply(FallingUnit unit)
    {
        Destroy(unit.damager);
        var d = unit.gameObject.AddComponent<UnitShielderDamager>();
        unit.damager = d;
        d.shieldFactor = shieldFactor;
    }

    public override void Remove(FallingUnit unit)
    {
        Destroy(unit.damager);
        unit.gameObject.AddComponent<UnitDamager>();
        unit.damager = GetComponent<UnitDamager>();
    }
}
