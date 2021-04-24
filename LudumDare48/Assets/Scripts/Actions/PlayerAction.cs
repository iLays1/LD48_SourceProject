using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAction : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    LaneManager laneManager;
    public UnitAction action;

    private void Awake()
    {
        laneManager = FindObjectOfType<LaneManager>();
        action = GetComponent<UnitAction>();
    }

    public void DoLeft(FallingUnit player)
    {
        if(action != null)
            action.Do(player, -1);
    }
    public void DoRight(FallingUnit player)
    {
        if (action != null)
            action.Do(player, 1);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick.Invoke();
    }
}
