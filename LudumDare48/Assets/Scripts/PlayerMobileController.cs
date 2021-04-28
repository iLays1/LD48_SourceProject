using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
    public FallingPlayer player;
    public float edgeDis = 10f;
    Camera cam;
    [HideInInspector]
    public bool isActive = true;

    private void Awake()
    {
        cam = Camera.main;
        LevelEndHandler.OnLevelWin.AddListener(() => isActive = false);
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(1);
            if (touch.phase == TouchPhase.Began)
            {
                player.StayAction();
            }
            return;
        }
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            Debug.Log(touchPos.x);
            if(touch.phase == TouchPhase.Began)
            {
                if (touchPos.x < -edgeDis)
                    player.LeftAction();
                if (touchPos.x > edgeDis)
                    player.RightAction();
            }
            return;
        }
    }
}
