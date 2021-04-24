using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlayer : FallingUnit
{
    public static UnityEvent OnAction = new UnityEvent();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
            OnAction.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
            OnAction.Invoke();
        }
    }

    public override void SetLane(int index)
    {
        if (index < 0) index = 0;
        if (index > lanes.Length - 1) index = lanes.Length - 1;

        if (lanes[index].occupant == null)
        {
            lanes[laneIndex].occupant = null;
            lanes[index].occupant = this;

            laneIndex = index;

            transform.DOKill();
            transform.DOMove(laneManager.lanes[index].transform.position, 0.2f);
        }
        else
        {
            //Hit Enemy
            if (lanes[index].occupant != null)
            {
                if (index < laneIndex)
                {
                    SkillSelection.selectedAction.DoLeft(this);
                    return;
                }
                if (index > laneIndex)
                {
                    SkillSelection.selectedAction.DoRight(this);
                    return;
                }
            }

            //Hit wall
            if (index == lanes.Length - 1)
            {
                transform.DOPunchPosition(Vector3.right * 0.5f, 0.1f);
                return;
            }
            if (index == 0)
            {
                transform.DOPunchPosition(Vector3.left * 0.5f, 0.1f);
                return;
            }
        }
    }
}
