using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionTimerText : MonoBehaviour
{
    public TextMeshProUGUI text;
    TickManager tickManager;

    private void Awake()
    {
        tickManager = FindObjectOfType<TickManager>();
        TickManager.OnTick.AddListener(Tick);
    }
    void Tick()
    {
        text.text = tickManager.ticksTillMove.ToString();
    }
}
