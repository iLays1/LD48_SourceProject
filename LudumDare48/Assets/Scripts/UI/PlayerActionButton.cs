using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActionButton : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    public UnitAction action;

    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
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

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                if(touchPos.x < transform.position.x + (col.bounds.size.x/2) && 
                   touchPos.x > transform.position.x - (col.bounds.size.x / 2) &&
                   touchPos.y < transform.position.y + (col.bounds.size.y / 2) &&
                   touchPos.y > transform.position.y - (col.bounds.size.y / 2))
                {
                    MainMobileController.CallCancel.Invoke();
                    OnClick.Invoke();
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick.Invoke();
        }
    }
}
