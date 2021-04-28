using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActionButton : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    public UnitAction action;

    private void Awake()
    {
        action = GetComponent<UnitAction>();
    }

    public bool DoLeft(FallingUnit player)
    {
        if(action != null)
            return action.Do(player, -1);

        return false;
    }
    public bool DoRight(FallingUnit player)
    {
        if (action != null)
            return action.Do(player, 1);

        return false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick.Invoke();
    }
}
