using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShielderDamager : UnitDamager
{
    public float shieldFactor;
    FallingPlayer player;

    public override void Awake()
    {
        player = FindObjectOfType<FallingPlayer>();
        base.Awake();
    }

    public override void TakeAttackDamage(int damage, int sourceIndex)
    {
        damage -= u.defense;

        if(u.GetDirFrom(sourceIndex) == u.facingDir)
            damage = Mathf.RoundToInt(damage * shieldFactor);

        if (damage < 0) damage = 0;

        TakeDamage(damage);
    }
}
