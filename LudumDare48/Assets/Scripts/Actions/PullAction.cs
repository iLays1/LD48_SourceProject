using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAction : UnitAction
{
    public override void Do(FallingUnit user, int dir)
    {
        var pLane = LaneManager.instance.lanes[user.laneIndex];
        var tLane = LaneManager.instance.lanes[user.laneIndex + dir];
        var target = tLane.occupant;
        
        if (dir == 1)
        {
            if (user.laneIndex > 0 && LaneManager.instance.lanes[user.laneIndex - 1].occupant == null)
            {
                user.SetLane(user.laneIndex - 1);
                target.SetLane(user.laneIndex + 1);
            }
        }
        if (dir == -1)
        {
            if (user.laneIndex < LaneManager.instance.lanes.Length - 1 && LaneManager.instance.lanes[user.laneIndex + 1].occupant == null)
            {
                user.SetLane(user.laneIndex + 1);
                target.SetLane(user.laneIndex - 1);
            }
        }
    }
}
