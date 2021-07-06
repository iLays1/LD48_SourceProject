using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    public TextMeshProUGUI atkStatText;
    public TextMeshProUGUI defStatText;
    public TextMeshProUGUI spdStatText;

    FallingPlayer player; //this might change later into a player stats object

    private void Awake()
    {
        player = FindObjectOfType<FallingPlayer>();
        UpdateStats();
    }

    public void UpdateStats()
    {
        if (atkStatText != null)
            atkStatText.text = $"Atk {player.attackPower}";
        if (defStatText != null)
            defStatText.text = $"Def {player.defense}";
        if (spdStatText != null)
            spdStatText.text = $"Spd {player.speed}";
    }
}
