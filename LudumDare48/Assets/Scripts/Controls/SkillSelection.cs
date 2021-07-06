using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    public static PlayerActionButton selectedAction;
    public PlayerActionButton[] skills;
    int selectionIndex = 0;
    public AudioSource source;

    public float baseY;
    float selectedY;
    
    private void Start()
    {
        baseY = skills[0].transform.localScale.y;
        selectedY = baseY + 0.1f;

        for (int i = 0; i < skills.Length; i++)
        {
            int num = i;
            skills[i].OnClick.AddListener(() => SetSkill(num));

            skills[i].transform.DOKill();
            skills[i].transform.DOScale(baseY, 0.5f);

            skills[i].LoadButton(PlayerEquipment.instance.actions[i]);
        }

        SetSkill(selectionIndex, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
            MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.E))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetSkill(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetSkill(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SetSkill(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            SetSkill(5);
    }

    public void MoveLeft()
    {
        SetSkill(selectionIndex - 1);
    }
    public void MoveRight()
    {
        SetSkill(selectionIndex + 1);
    }

    public void SetSkill(int index, bool playSound = true)
    {
        if (!skills[index].active) return;

        if (playSound) source.Play();
        
        if (index < 0) index = skills.Length - 1;
        if (index > skills.Length - 1) index = 0;

        var os = skills[selectionIndex].transform;
        var s = skills[index].transform;

        os.DOKill();
        os.DOScale(baseY, 0.5f);
        s.DOKill();
        s.DOScale(selectedY, 0.1f);

        selectionIndex = index;
        selectedAction = skills[selectionIndex];
    }
}
