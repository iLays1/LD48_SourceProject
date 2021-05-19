using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    
    private void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (touch.phase == TouchPhase.Began)
            {
                float extra = 0.1f;

                if (touchPos.x < transform.position.x + (col.bounds.size.x / 2) + extra && 
                   touchPos.x > transform.position.x - (col.bounds.size.x / 2) - extra &&
                   touchPos.y < transform.position.y + (col.bounds.size.y / 2) + extra &&
                   touchPos.y > transform.position.y - (col.bounds.size.y / 2) - extra)
                {
                    //Debug.Log(name);
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
