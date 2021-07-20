using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FallingEnemy))]
public abstract class EnemyBehavior : MonoBehaviour
{
    protected Lane[] lanes { get { return LaneManager.instance.lanes; } }
    protected FallingEnemy enemy;

    public UnitAction action;
    public float waitTime = 0.25f;
    protected WaitForSeconds WaitForTime;

    private void Awake()
    {
        WaitForTime = new WaitForSeconds(waitTime);
        enemy = GetComponent<FallingEnemy>();   
    }
    
    public virtual void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return WaitForTime;
        BasicAct();
    }

    //Defualt action
    protected void BasicAct()
    {
        if (enemy.facingDir == -1)
        {
            if (enemy.visuals != null)
                enemy.visuals.FlipLeft();
            if (lanes[enemy.laneIndex - 1].occupant != null && lanes[enemy.laneIndex - 1].occupant is FallingPlayer)
            {
                action.Do(enemy, -1);
            }
            else
            {
                enemy.visuals.MoveAnim();
                enemy.MoveLeft();
            }
        }
        if (enemy.facingDir == 1)
        {
            if (enemy.visuals != null)
                enemy.visuals.FlipRight();

            if (lanes[enemy.laneIndex + 1].occupant != null && lanes[enemy.laneIndex + 1].occupant is FallingPlayer)
            {
                action.Do(enemy, +1);
            }
            else
            {
                enemy.visuals.MoveAnim();
                enemy.MoveRight();
            }
        }
    }
}
