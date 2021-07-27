using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ilpDragSlots;

public class SkillEquipingCanvas : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] hiders;

    public DataSlotCollection storage;
    public DataSlotCollection skillBar;

    public SkillSelection skillSelectionBar;

    PlayerMobileController controller;
    WaitForSeconds startDelay = new WaitForSeconds(0.5f);

    private void Awake()
    {
        controller = FindObjectOfType<PlayerMobileController>();
    }

    private void Start()
    {
        Begin();
    }

    public void Begin()
    {
        controller.isActive = false;
        panel.SetActive(true);
        foreach (var h in hiders)
            h.SetActive(false);
    }
    public void End()
    {
        int count = -1;
        foreach (var slot in storage.slots)
        {
            count++;
            if (count >= PlayerEquipment.instance.availableActions.Length) continue;
            if (slot == null || slot.contatinedData == null)
            {
                PlayerEquipment.instance.availableActions[count] = null;
                continue;
            }

            PlayerEquipment.instance.availableActions[count] = slot.contatinedData.GetObject().GetComponent<SkillDataIcon>().action;
        }
        count = -1;
        foreach (var slot in skillBar.slots)
        {
            count++;
            if (count >= PlayerEquipment.instance.availableActions.Length) continue;
            if (slot == null || slot.contatinedData == null)
            {
                PlayerEquipment.instance.selectedActions[count] = null;
                continue;
            }

            PlayerEquipment.instance.selectedActions[count] = slot.contatinedData.GetObject().GetComponent<SkillDataIcon>().action;
        }

        skillSelectionBar.LoadSkills();

        panel.SetActive(false);
        foreach (var h in hiders)
            h.SetActive(true);

        TickManager.ActivateHazards.Invoke();

        StartCoroutine(WaitForMovement());
    }
    IEnumerator WaitForMovement()
    {
        yield return startDelay;

        controller.isActive = true;
    }
}
