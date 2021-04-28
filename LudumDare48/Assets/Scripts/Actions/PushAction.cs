using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAction : UnitAction
{
    public override bool Do(FallingUnit user, int dir)
    {
        var pLane = LaneManager.instance.lanes[user.laneIndex];
        var tLane = LaneManager.instance.lanes[user.laneIndex + dir];
        var target = tLane.occupant;
        
        if (dir == 1)
        {
            if (user.laneIndex + 1 < LaneManager.instance.lanes.Length - 1 && LaneManager.instance.lanes[user.laneIndex + 2].occupant == null)
            {
                target.SetLane(user.laneIndex + 2);
                //user.SetLane(user.laneIndex + 1);
                return true;
            }
        }
        if (dir == -1)
        {
            if(user.laneIndex - 1 > 0 && LaneManager.instance.lanes[user.laneIndex - 2].occupant == null)
            {
                target.SetLane(user.laneIndex - 2);
                //user.SetLane(user.laneIndex - 1);
                return true;
            }
        }
        return false;
    }
}
