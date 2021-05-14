using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIHider : MonoBehaviour
{
    [SerializeField] Vector3 HideOffset;

    Vector3 startPos;
    RectTransform rec;

    private void Awake()
    {
        LevelEndHandler.OnLevelWin.AddListener(HideUI);
        LevelEndHandler.OnLevelLose.AddListener(HideUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            HideUI();
        if (Input.GetKeyDown(KeyCode.Y))
            ShowUI();
    }

    private void Start()
    {
        rec = GetComponent<RectTransform>();
        startPos = rec.position;
    }

    public void HideUI()
    {
        rec.DOMove(startPos + HideOffset, 0.5f);
    }
    public void ShowUI()
    {
        rec.DOMove(startPos, 0.5f);
    }

}
