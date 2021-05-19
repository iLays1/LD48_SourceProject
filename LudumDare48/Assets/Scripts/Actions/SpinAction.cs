using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Spin")]
public class SpinAction : UnitAction
{
    public override bool Do(FallingUnit user, int dir)
    {
        var lanes = LaneManager.instance.lanes;
        var pLane = lanes[user.laneIndex];
        var tLane = lanes[user.laneIndex + dir];
        var target = tLane.occupant;
        
        if (dir == 1)
        {
            if (user.laneIndex > 0 && lanes[user.laneIndex - 1].occupant == null)
            {
                target.SetLane(user.laneIndex - 1);
                return true;
            }
        }
        if (dir == -1)
        {
            if (user.laneIndex < lanes.Length - 1 && lanes[user.laneIndex + 1].occupant == null)
            {
                target.SetLane(user.laneIndex + 1);
                return true;
            }
        }
        return false;
    }
}
