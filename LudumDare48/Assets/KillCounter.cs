using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    TextMeshProUGUI text;
    int kills = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);
        text.text = $"{kills}";
    }

    void OnKill()
    {
        kills++;
        text.text = $"{kills}";
    }
}
