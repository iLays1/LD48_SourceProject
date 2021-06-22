using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlayer : FallingUnit
{
    public static UnityEvent OnAction = new UnityEvent();
    public bool isActive = true;
    public bool keyboardControls = true;
    bool moving = false;

    WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
    WaitForSeconds waitForMoveLimiter = new WaitForSeconds(0.05f);

    public void LeftAction()
    {
        if (moving || !isActive) return;
        
        if (SkillSelection.selectedAction == null || !SkillSelection.selectedAction.action.stationaryAction)
        {
            MoveLeft();
        }
        else
        {
            visuals.FlipLeft();
            UseSkillLeft();
        }
    }
    public void RightAction()
    {
        if (moving || !isActive) return;

        if (SkillSelection.selectedAction == null || !SkillSelection.selectedAction.action.stationaryAction)
        {
            MoveRight();
        }
        else
        {
            visuals.FlipRight();
            UseSkillRight();
        }
    }

    public void UseSkillLeft(bool tickless = false)
    {
        bool skillUsed = SkillSelection.selectedAction.UseAction(this, -1);
        if (skillUsed && !tickless) TickAction();
        else
            transform.DOPunchPosition(Vector3.left * 0.5f, 0.1f);
    }
    public void UseSkillRight(bool tickless = false)
    {
        bool skillUsed = SkillSelection.selectedAction.UseAction(this, 1);
        if (skillUsed && !tickless) TickAction();
        else
            transform.DOPunchPosition(Vector3.right * 0.5f, 0.1f);
    }

    public void StayAction()
    {
        if (moving || !isActive) return;
        
        myLane.OnPlayerInteract(this);

        TickAction();
    }

    public void LimitInputs()
    {
        StartCoroutine(LimitInputsCoroutine());
    }
    IEnumerator LimitInputsCoroutine()
    {
        moving = true;
        yield return waitForMoveLimiter;
        moving = false;
    }

    void TickAction() => StartCoroutine(TickActionCoroutine());
    IEnumerator TickActionCoroutine()
    {
        yield return endOfFrame;

        LimitInputs();
        OnAction.Invoke();
    }
    
    public override void SetLane(int index, bool tick = false)
    {
        if (index < 0) index = 0;
        if (index > lanes.Length - 1) index = lanes.Length - 1;

        if (lanes[index].occupant == null)
        {
            myLane.occupant = null;
            lanes[index].occupant = this;

            laneIndex = index;

            unitAudio?.moveSound.Play();

            if(tick) TickAction();

            transform.DOKill();
            transform.DOMove(myLane.transform.position, 0.2f);
        }
        else
        {
            //Hit Enemy
            if (lanes[index].occupant != null)
            {
                if (index < laneIndex)
                {
                    UseSkillLeft();
                    return;
                }
                if (index > laneIndex)
                {
                    UseSkillRight();
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
