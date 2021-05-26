using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneUpgradeInteractable : LaneInteractable
{
    public UpgradeInfoPanel panel;
    bool occupied = false;

    private void Awake()
    {
        MainMobileController.OnTapLeft.AddListener(panel.Hide);
        MainMobileController.OnTapRight.AddListener(panel.Hide);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        panel.locked = true;
        FindObjectOfType<FallingPlayer>().isActive = false;
    }

    private void Update()
    {
        if(targetLane.occupant != null && !occupied)
        {
            occupied = true;
            panel.ShowText(debugString);
        }
        else if (targetLane.occupant == null)
        {
            occupied = false;
        }
    }
}
