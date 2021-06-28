using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : LaneModifier
{
    public override void Initalize()
    {
        TickManager.ActivateHazards.AddListener(Activate);
        LevelEndHandler.OnLevelWin.AddListener(RemoveModifier);
        targetLane.UpdateColor();

        base.Initalize();
    }

    protected abstract void Activate();
}
