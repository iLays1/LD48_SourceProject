using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAction : UnitAction
{
    public override bool Do(FallingUnit user, int dir)
    {
        var target = FindObjectOfType<FallingPlayer>();

        dir = target.laneIndex > user.laneIndex ? 1 : -1;

        user.transform.DOPunchPosition(new Vector3(dir, 0, 0) * 0.8f, 0.1f);

        if (dir == -1)
            user.visuals.FlipLeft();
        if (dir == 1)
            user.visuals.FlipRight();

        target.TakeDamage(user.attackPower);

        user.unitAudio?.attackSound.Play();
        if (user.visuals != null)
            user.visuals.AttackAnimation();

        return true;
    }
}
