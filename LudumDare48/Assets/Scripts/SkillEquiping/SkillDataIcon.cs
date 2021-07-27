using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ilpDragSlots;
using DG.Tweening;
using TMPro;

public class SkillDataIcon : MonoBehaviour, IDataSlotObject
{
    public UnitAction action;
    public TextMeshProUGUI text;
    Vector3 oScale;
    RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        oScale = rt.localScale;
    }

    public void LoadAction(UnitAction action)
    {
        this.action = action;
        text.text = action.displayName;
    }

    public void OnDropped(DataSlot droppedSlot)
    {
        rt.DOScale(oScale, 0.2f);
    }

    public void OnSelected()
    {
        rt.DOScale(oScale * 1.2f, 0.2f);
    }

    public void SetDisplayedToSlot(DataSlot slot)
    {
        rt.DOComplete();
        rt.DOMove(slot.GetComponent<RectTransform>().position, 0.1f);
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}
