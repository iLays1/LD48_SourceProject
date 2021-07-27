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

        public void Initalize(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            dataSlotObject = obj;

            contatinedData = dataSlotObject.GetComponent<IDataSlotObject>();
            parentCollection = GetComponentInParent<DataSlotCollection>();

            Invoke("RefreshDataSlot", 0.05f);
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
        public void LoadData()
        {
            contatinedData = dataSlotObject.GetComponent<IDataSlotObject>();
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
