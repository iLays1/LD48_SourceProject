using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/HazardSelfAction")]
public class HazardSelfAction : UnitAction
{
    public PotionHazard hazard;
    public override bool stationaryAction { get { return true; } }

    public override bool Do(FallingUnit user, int dir)
    {
        var haz = Instantiate(hazard);
        haz.targetLane = LaneManager.instance.lanes[user.laneIndex];
        haz.Initalize();

        return true;
    }
}
