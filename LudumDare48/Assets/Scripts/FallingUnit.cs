using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingUnit : MonoBehaviour
{
    public int laneIndex;
    int lastIndex;

    protected LaneManager laneManager;
    protected Lane[] lanes { get { return laneManager.lanes; } }

    private void Awake()
    {
        laneManager = FindObjectOfType<LaneManager>();
        lastIndex = laneIndex;
        SetLane(laneIndex);
    }

    public void MoveLeft()
    {
        SetLane(laneIndex - 1);
    }
    public void MoveRight()
    {
        SetLane(laneIndex + 1);
    }

    public virtual void SetLane(int index)
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
            if(index > laneIndex || index == lanes.Length - 1)
            {
                transform.DOPunchPosition(Vector3.right * 0.5f, 0.1f);
            }
            if(index < laneIndex || index == 0)
            {
                transform.DOPunchPosition(Vector3.left * 0.5f, 0.1f);
            }
        }
    }
}
