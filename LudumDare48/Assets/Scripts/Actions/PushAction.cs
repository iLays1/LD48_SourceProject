using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAction : UnitAction
{
    public override void Do(FallingUnit user, int dir)
    {
        var pLane = laneManager.lanes[user.laneIndex];
        var tLane = laneManager.lanes[user.laneIndex + dir];
        var target = tLane.occupant;

        //Hit wall
        if (dir == 1)
        {
            if (user.laneIndex + 1 < laneManager.lanes.Length - 1)
            {
                target.SetLane(user.laneIndex + 2);
                user.SetLane(user.laneIndex + 1);
            }
        }
        if (dir == -1)
        {
            if(user.laneIndex - 1 > 0)
            {
                target.SetLane(user.laneIndex - 2);
                user.SetLane(user.laneIndex - 1);
            }
        }
    }
}
