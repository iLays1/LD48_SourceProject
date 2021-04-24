using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAction : UnitAction
{
    public override void Do(FallingUnit user, int dir)
    {
        var pLane = laneManager.lanes[user.laneIndex];
        var tLane = laneManager.lanes[user.laneIndex + dir];

        pLane.occupant = null;
        tLane.occupant.SetLane(user.laneIndex);
        user.laneIndex = user.laneIndex + dir;
        user.SetLane(user.laneIndex);
    }
}
