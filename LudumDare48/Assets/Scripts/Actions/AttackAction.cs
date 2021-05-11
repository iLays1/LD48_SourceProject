using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : UnitAction
{
    public override bool Do(FallingUnit user, int dir)
    {
        var target = LaneManager.instance.lanes[user.laneIndex + dir].occupant;

        user.transform.DOPunchPosition(new Vector3(dir,0,0) * 0.8f, 0.1f);

        int dmg = user.attackPower - target.defense;
        if (dmg < 0) dmg = 0;
        target.TakeDamage(dmg);

        user.unitAudio?.attackSound.Play();
        if (user.visuals != null)
            user.visuals.AttackAnimation();
        
        return true;
    }
}
