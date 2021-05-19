using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Ranged")]
public class RangedAttackAction : UnitAction
{
    public float damageFactor = 0.2f;

    private void Awake()
    {
        stationaryAction = true;
    }

    public override bool Do(FallingUnit user, int dir)
    {
        int ui = user.laneIndex;
        var lanes = LaneManager.instance.lanes;

        if (dir == 0) return false;

        int i = ui + dir;
        FallingUnit target = null;
        
        while (i >= 0 && i < lanes.Length)
        {
            if (lanes[i].occupant != null)
            {
                target = lanes[i].occupant;
                break;
            }

            i += dir;
        }

        if(target != null)
        {
            int dmg = Mathf.RoundToInt(user.attackPower * damageFactor);
            if (dmg < 1) dmg = 1;

            user.visuals.AttackAnimation();
            target.TakeAttackDamage(dmg);
            return true;
        }

        return false;
    }
}
