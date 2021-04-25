using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TickManager : MonoBehaviour
{
    public static UnityEvent OnTick = new UnityEvent();
    public static UnityEvent OnOutOfMoves = new UnityEvent();

    public int baseTicksTillMove = 5;
    public int ticksTillMove = 0;

    private void Awake()
    {
        FallingPlayer.OnAction.AddListener(Tick);
    }
    private void Start()
    {
        ticksTillMove = baseTicksTillMove;
        OnTick.Invoke();
    }

    void Tick()
    {
        ticksTillMove--;

        if (ticksTillMove <= 0)
        {
            ticksTillMove = baseTicksTillMove;
            OnOutOfMoves.Invoke();
        }

        OnTick.Invoke();
    }
}
