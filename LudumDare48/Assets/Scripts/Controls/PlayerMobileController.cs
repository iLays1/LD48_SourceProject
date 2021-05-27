using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileController : MonoBehaviour
{
    public FallingPlayer player;
    public bool isActive = true;

    private void Awake()
    {
        LevelEndHandler.OnLevelWin.AddListener(() => isActive = false);
        LevelEndHandler.OnLevelLose.AddListener(() => isActive = false);

        MainMobileController.OnTapLeft.AddListener(Left);
        MainMobileController.OnTapRight.AddListener(Right);
        MainMobileController.OnDoubleTap.AddListener(DoubleTap);
    }

    private void Start()
    {
        player = FindObjectOfType<FallingPlayer>();
    }
    
    void Left()
    {
        if (!isActive) return;
        player.LeftAction();
    }
    void Right()
    {
        if (!isActive) return;
        player.RightAction();
    }
    void DoubleTap()
    {
        if (!isActive) return;
        player.StayAction();
    }
}
