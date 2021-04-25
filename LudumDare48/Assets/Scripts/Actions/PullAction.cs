using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAction : UnitAction
{
    public override void Do(FallingUnit user, int dir)
    {
        var pLane = laneManager.lanes[user.laneIndex];
        var tLane = laneManager.lanes[user.laneIndex + dir];
        var target = tLane.occupant;
        
        if (dir == 1)
        {
            if (user.laneIndex > 0 && laneManager.lanes[user.laneIndex - 1].occupant == null)
            {
                user.SetLane(user.laneIndex - 1);
                target.SetLane(user.laneIndex + 1);
            }
        }
        if (dir == -1)
        {
            if (user.laneIndex < laneManager.lanes.Length - 1 && laneManager.lanes[user.laneIndex + 1].occupant == null)
            {
                user.SetLane(user.laneIndex + 1);
                target.SetLane(user.laneIndex - 1);
            }
        }
    }
}
