using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ilpDragSlots
{
    public class DataSlotCollection : MonoBehaviour
    {
        public DataSlot[] slots;

        public GameObject[] GetData()
        {
            List<GameObject> list = new List<GameObject>();

            foreach (var slot in slots)
            {
                if (slot.contatinedData != null)
                    list.Add(slot.contatinedData.GetObject());
                else
                    list.Add(null);
            }

            return list.ToArray();
        }
    }
}
