using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetCanvas : MonoBehaviour
{
    public Canvas canvasToShow;
    public Canvas canvasToHide;

    public void CLICKED()
    {
        canvasToShow.gameObject.SetActive(true);
        canvasToHide.gameObject.SetActive(false);
    }
}
