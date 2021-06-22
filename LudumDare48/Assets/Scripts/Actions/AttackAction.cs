using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/BasicAttack")]
public class AttackAction : UnitAction
{
    public float damageFactor = 1f;

    public override bool Do(FallingUnit user, int dir)
    {
        var target = LaneManager.instance.lanes[user.laneIndex + dir].occupant;

        user.transform.DOPunchPosition(new Vector3(dir,0,0) * 0.8f, 0.1f);

        int damage = Mathf.RoundToInt(user.attackPower * damageFactor);
        target.damager.TakeAttackDamage(damage, user.laneIndex);

        user.unitAudio?.attackSound.Play();
        if (user.visuals != null)
            user.visuals.ActAnimation();
        
        return true;
    }
}
