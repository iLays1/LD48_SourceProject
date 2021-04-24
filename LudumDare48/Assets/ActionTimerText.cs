using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionTimerText : MonoBehaviour
{
    public TextMeshProUGUI text;
    int tempMaxTime = 10;
    int tempTime = 10;

    private void Awake()
    {
        FallingPlayer.OnAction.AddListener(Tick);
    }
    void Tick()
    {
        tempTime--;

        if (tempTime <= 0)
        {
            tempTime = tempMaxTime;
            Debug.Log("AAA");
        }

        text.text = tempTime.ToString();
    }
}
