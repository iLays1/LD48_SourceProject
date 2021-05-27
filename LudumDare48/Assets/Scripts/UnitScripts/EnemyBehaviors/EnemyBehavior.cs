using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FallingEnemy))]
public abstract class EnemyBehavior : MonoBehaviour
{
    protected Lane[] lanes { get { return LaneManager.instance.lanes; } }
    protected FallingEnemy enemy;

    public UnitAction action;

    private void Awake()
    {
        enemy = GetComponent<FallingEnemy>();   
    }
    
    public virtual void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
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
                enemy.MoveRight();
            }
        }
    }
}
