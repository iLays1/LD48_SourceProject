using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ilpDragSlots
{
    public interface IDataSlotObject
    {
        void SetDisplayedToSlot(DataSlot slot);
        void OnSelected();
        void OnDropped(DataSlot droppedSlot);
        GameObject GetObject();
    }
}
