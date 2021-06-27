using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobileButton : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
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
                    ClickButton();
                }
            }
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickButton();
        }
    }

    public void ClickButton()
    {
        MainMobileController.CallCancel.Invoke();
        OnClick.Invoke();
    }

}
