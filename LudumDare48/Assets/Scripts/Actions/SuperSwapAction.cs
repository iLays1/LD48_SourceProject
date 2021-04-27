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

        dir = target.laneIndex > user.laneIndex ? -1 : 1;
        
        if (dir == -1)
            user.visuals.FlipLeft();
        if (dir == 1)
            user.visuals.FlipRight();

        if (user.visuals != null)
            user.visuals.AttackAnimation();

        pLane.occupant = null;
        tLane.occupant.SetLane(ui);

        user.laneIndex = ti;
        user.SetLane(user.laneIndex);
    }
}
