using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public static HpSlider Create(FallingUnit unit)
    {
        var go = (GameObject)Instantiate(Resources.Load("HpSlider"));
        go.transform.SetParent(GameObject.Find("WorldCanvas").transform);
        
        HpSlider hpSlider = go.GetComponent<HpSlider>();
        
        hpSlider.unit = unit;
        hpSlider.slider.maxValue = unit.maxHp;
        hpSlider.slider.value = unit.hp;
        hpSlider.UpdateUI();

        unit.OnHpChange.AddListener(hpSlider.UpdateUI);
        unit.OnDeath.AddListener(() => Destroy(hpSlider.gameObject));
        return hpSlider;
    }

    public FallingUnit unit;
    public Slider slider;
    public Vector3 offset;
    public TextMeshProUGUI text;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (unit == null) Destroy(gameObject);
    }

    private void Update()
    {
        if (unit == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = unit.transform.position + offset;
    }

    public void UpdateUI()
    {
        slider.maxValue = unit.maxHp;
        slider.value = unit.hp;
        text.text = unit.hp.ToString();
    }
}
