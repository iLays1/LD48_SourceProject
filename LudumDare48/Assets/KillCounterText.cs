using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounterText : MonoBehaviour
{
    LevelManager lm;
    TextMeshProUGUI text;
    
    [SerializeField] Gradient textGradient;

    private void Awake()
    {
        lm = FindObjectOfType<LevelManager>();
        text = GetComponent<TextMeshProUGUI>();

        lm.OnUpdateUI.AddListener(SetText);
    }

    void SetText()
    {
        if (lm.winTarget != null)
        {
            text.color = Color.white;
            text.text = $"Defeat the Boss";
        }
        else
        {
            if (lm.kills > lm.killsToWin) lm.kills = lm.killsToWin;

            text.text = $"{lm.killsToWin - lm.kills}<size=20>\n Kills left";
            text.color = textGradient.Evaluate((float)lm.kills / lm.killsToWin);
        }
    }
}
