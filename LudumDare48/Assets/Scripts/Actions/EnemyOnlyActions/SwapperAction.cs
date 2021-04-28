using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapperAction : UnitAction
{
    public override bool Do(FallingUnit user, int dir)
    {
        var target = FindObjectOfType<FallingPlayer>();
        if (target == null) return false;

        int ui = user.laneIndex;
        int ti = target.laneIndex;

        var pLane = LaneManager.instance.lanes[ui];
        var tLane = LaneManager.instance.lanes[ti];

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

        return true;
    }
}
