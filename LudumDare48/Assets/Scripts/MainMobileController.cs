using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMobileController : MonoBehaviour
{
    public static UnityEvent OnTapLeft = new UnityEvent();
    public static UnityEvent OnTapRight = new UnityEvent();
    public static UnityEvent OnDoubleTap = new UnityEvent();
    public static UnityEvent CallCancel = new UnityEvent();

    public float edgeDis = 10f;
    Camera cam;
    bool actionValid = true;

    private void Awake()
    {
        cam = Camera.main;
        CallCancel.AddListener(CancelAction);
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (touchPos.x < -edgeDis)
                    CancelableAction(() => OnTapLeft.Invoke());
                if (touchPos.x > edgeDis)
                    CancelableAction(() => OnTapRight.Invoke());
            }
        }

        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began)
            {
                OnDoubleTap.Invoke();
            }
            actionValid = false;
            return;
        }
    }

    public void CancelAction()
    {
        actionValid = false;
    }
    
    public void CancelableAction(UnityAction action)
    {
        StartCoroutine(CancelableActionCoroutine(action));
    }
    IEnumerator CancelableActionCoroutine(UnityAction action)
    {
        actionValid = true;
        yield return new WaitForSeconds(0.075f);
        if(actionValid) action.Invoke();
    }
}
