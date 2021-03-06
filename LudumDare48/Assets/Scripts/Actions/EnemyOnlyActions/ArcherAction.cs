using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Enemy_ArcherAction")]
public class ArcherAction : UnitAction
{
    public ArrowHazard arrowHazardPrefab;

    public override bool Do(FallingUnit user, int dir)
    {
        var target = FindObjectOfType<FallingPlayer>();

        dir = target.laneIndex > user.laneIndex ? 1 : -1;

        user.transform.DOPunchPosition(new Vector3(dir, 0, 0) * 0.8f, 0.1f);

        if (dir == -1)
            user.visuals.FlipLeft();
        if (dir == 1)
            user.visuals.FlipRight();

        var arrow = Instantiate(arrowHazardPrefab);

        arrow.damage = user.attackPower;
        arrow.targetLane = LaneManager.instance.lanes[target.laneIndex];
        arrow.Initalize();

        if (user.visuals != null)
            user.visuals.ActAnimation();
        
        user.unitAudio?.attackSound.Play();

        return true;
    }
}
