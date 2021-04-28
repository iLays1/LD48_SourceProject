using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlayer : FallingUnit
{
    public static UnityEvent OnAction = new UnityEvent();
    public bool isActive = true;

    protected override void Awake()
    {
        base.Awake();
        //TickManager.OnOutOfMoves.AddListener(() => isActive = false);
    }

    private void Update()
    {
        if(isActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LeftAction();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                RightAction();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                StayAction();
            }
        }
    }

    public void LeftAction()
    {
        MoveLeft();
        TickAction();
    }
    public void RightAction()
    {
        MoveRight();
        TickAction();
    }
    public void StayAction()
    {
        TickAction();
    }

    void TickAction() => StartCoroutine(TickActionCoroutine());
    IEnumerator TickActionCoroutine()
    {
        yield return new WaitForEndOfFrame();

        OnAction.Invoke();
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
            transform.DOMove(LaneManager.instance.lanes[index].transform.position, 0.2f);
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
                return;
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
