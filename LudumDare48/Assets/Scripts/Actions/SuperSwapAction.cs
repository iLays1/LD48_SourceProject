using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSwapAction : UnitAction
{
    public override void Do(FallingUnit user, int dir)
    {
        var target = FindObjectOfType<FallingPlayer>();

        int ui = user.laneIndex;
        int ti = target.laneIndex;

        var pLane = laneManager.lanes[ui];
        var tLane = laneManager.lanes[ti];

        pLane.occupant = null;
        tLane.occupant.SetLane(ui);

        user.laneIndex = ti;
        user.SetLane(user.laneIndex);
    }
}
