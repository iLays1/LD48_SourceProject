using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ilpDragSlots;
using UnityEngine.Events;

namespace ilpDragSlots
{
    public class DataSlot : MonoBehaviour
    {
        public bool isLocked = false;
        public DataSlotCollection parentCollection;
        public GameObject dataSlotObject;
        public IDataSlotObject contatinedData;

        private void Awake()
        {
            if (dataSlotObject == null)
            {
                return;
            }

            contatinedData = dataSlotObject.GetComponent<IDataSlotObject>();
            if (contatinedData == null)
            {
                Debug.LogWarning($"{name} has no dataSlotObject Component");
            }

            parentCollection = GetComponentInParent<DataSlotCollection>();
        }
        private void Start()
        {
            RefreshDataSlot();
        }

        public void SelectSlot()
        {
            if(contatinedData != null)
                contatinedData.OnSelected();
        }
        public void RefreshDataSlot()
        {
            if(contatinedData != null)
            {
                contatinedData.SetDisplayedToSlot(this);
                dataSlotObject = contatinedData.GetObject();
            }
            else
            {
                dataSlotObject = null;
            }
        }

        private void OnMouseEnter()
        {
            DragSlotsManager.hoveredSlot = this;
        }
        private void OnMouseExit()
        {
            DragSlotsManager.hoveredSlot = null;
        }
    }
}
