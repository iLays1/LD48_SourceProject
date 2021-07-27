using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ilpDragSlots;

public class PlayerEquipingScreen : MonoBehaviour
{
    public SkillDataIcon dataIconPrefab;

    public DataSlotCollection mainStorage;
    public DataSlotCollection skillBar;

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        int count = 0;
        foreach(var skill in PlayerEquipment.instance.availableActions)
        {
            if (skill == null)
            {
                continue;
            }

            var slot = mainStorage.slots[count];

            var go = Instantiate(dataIconPrefab, this.transform);
            var icon = go.GetComponent<SkillDataIcon>();
            slot.Initalize(go.gameObject);
            icon.LoadAction(skill);

            count++;
        }

        count = 0;
        foreach (var skill in PlayerEquipment.instance.selectedActions)
        {
            if (skill == null)
            {
                count++;
                continue;
            }

            var slot = skillBar.slots[count];

            var go = Instantiate(dataIconPrefab, this.transform);
            var icon = go.GetComponent<SkillDataIcon>();
            slot.Initalize(go.gameObject);
            icon.LoadAction(skill);

            count++;
        }
    }
}
