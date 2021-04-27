using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutPanels;
    public int index;

    public void RBUTTON()
    {
        tutPanels[index].gameObject.SetActive(false);
        index++;
        if (index >= tutPanels.Length)
            index = 0;
        tutPanels[index].gameObject.SetActive(true);
    }
    public void LBUTTON()
    {
        tutPanels[index].gameObject.SetActive(false);
        index--;
        if (index < 0)
            index = tutPanels.Length - 1;
        tutPanels[index].gameObject.SetActive(true);
    }
}
