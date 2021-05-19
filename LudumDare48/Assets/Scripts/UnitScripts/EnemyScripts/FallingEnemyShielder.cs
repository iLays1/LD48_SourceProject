using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemyShielder : FallingEnemy
{
    public override void TakeAttackDamage(int damage)
    {
        if (GetDirFrom(player) == facingDir)
            damage = 0;

        base.TakeAttackDamage(damage);
    }
}
