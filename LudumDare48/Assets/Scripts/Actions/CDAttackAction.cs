using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/CD_Attack")]
public class CDAttackAction : CoolDownAction
{
    public float dmgFactor = 2f;

    public override bool Do(FallingUnit user, int dir)
    {
        var target = LaneManager.instance.lanes[user.laneIndex + dir].occupant;

        user.transform.DOPunchPosition(new Vector3(dir, 0, 0) * 0.8f, 0.1f);

        int dmg = Mathf.RoundToInt(user.attackPower * dmgFactor);
        target.TakeAttackDamage(dmg);

        user.unitAudio?.attackSound.Play();
        if (user.visuals != null)
            user.visuals.AttackAnimation();

        return true;
    }
}
