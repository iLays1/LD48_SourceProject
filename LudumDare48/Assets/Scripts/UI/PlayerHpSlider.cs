using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSlider : MonoBehaviour
{
    public FallingUnit unit;
    public Slider slider;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (unit == null) unit = FindObjectOfType<FallingPlayer>();

        slider = GetComponent<Slider>();

        unit.OnHpChange.AddListener(UpdateUI);
        unit.OnDeath.AddListener(() => UpdateUIToZero());

        slider.maxValue = unit.maxHp;
        slider.value = unit.hp;
        
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        slider.maxValue = unit.maxHp;
        slider.value = unit.hp;
        text.text = $"Hp {unit.hp}/{unit.maxHp}";
    }
    void UpdateUIToZero()
    {
        slider.value = 0;
        text.text = "Dead";
    }
}
