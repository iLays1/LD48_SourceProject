using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
    public FallingPlayer player;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            Debug.Log(touchPos.x);
            if(touch.phase == TouchPhase.Began)
            {
                if (touchPos.x < -7)
                    player.LeftAction();
                if (touchPos.x > 7)
                    player.RightAction();
            }
        }
    }
}
