using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ilpDragSlots
{
    public class DragSlotsManager : MonoBehaviour
    {
        public static DataSlot selectedSlot = null;
        public static DataSlot hoveredSlot = null;
        public DataSlotCollection[] collections;

        bool tapLock = false;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && hoveredSlot != null)
            {
                SelectSlot(hoveredSlot);
                tapLock = true;
                Invoke("SetTapLockToFalse", 0.15f);
            }

            if (Input.GetMouseButtonUp(0) && hoveredSlot == null)
            {
                SelectSlot(selectedSlot);
                return;
            }

            if (Input.GetMouseButtonUp(0) && !tapLock && selectedSlot != null)
            {
                SelectSlot(hoveredSlot);
            }
        }
        void SetTapLockToFalse()
        {
            tapLock = false;
        }

        public static void SelectSlot(DataSlot slot)
        {
            if(slot == null || slot.isLocked)
            {
                if (selectedSlot != null)
                {
                    selectedSlot.contatinedData?.OnDropped(slot);
                    selectedSlot.RefreshDataSlot();
                    selectedSlot = null;
                }
                return;
            }
            if (selectedSlot == slot)
            {
                selectedSlot.contatinedData?.OnDropped(slot);
                selectedSlot.RefreshDataSlot();
                selectedSlot = null;
                return;
            }

            slot.SelectSlot();
            
            if (selectedSlot != null)
            {
                selectedSlot.contatinedData?.OnDropped(slot);
                slot.contatinedData?.OnDropped(selectedSlot);

                var data = selectedSlot.contatinedData;
                selectedSlot.contatinedData = slot.contatinedData;
                slot.contatinedData = data;

                selectedSlot.RefreshDataSlot();
                slot.RefreshDataSlot();
                
                selectedSlot = null;
                return;
            }

            if (slot.contatinedData == null) return;
            
            selectedSlot = slot;
        }
    }
}
