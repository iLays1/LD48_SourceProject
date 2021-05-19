using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Swap")]
public class SwapAction : UnitAction
{
    public override bool Do(FallingUnit user, int dir)
    {
        var pLane = LaneManager.instance.lanes[user.laneIndex];
        var tLane = LaneManager.instance.lanes[user.laneIndex + dir];

        pLane.occupant = null;
        tLane.occupant.SetLane(user.laneIndex);
        user.laneIndex = user.laneIndex + dir;
        user.SetLane(user.laneIndex);

        return true;
    }
}
