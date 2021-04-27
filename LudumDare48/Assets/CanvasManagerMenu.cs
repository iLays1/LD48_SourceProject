using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagerMenu : MonoBehaviour
{
    public static bool returnToCanvas = false;

    public Canvas[] hideThese;
    public Canvas ShowThis;

    private void Start()
    {
        if (returnToCanvas)
        {
            foreach(var c in hideThese)
            {
                c.gameObject.SetActive(false);
            }
            ShowThis.gameObject.SetActive(true);
        }
    }
}
