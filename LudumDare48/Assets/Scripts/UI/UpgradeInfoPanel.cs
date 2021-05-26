using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeInfoPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descTextMesh;
    public bool locked = false;

    public void ShowText(string s) //this s will eventually be an upgrade data
    {
        if (locked) return;
        gameObject.SetActive(true);
        descTextMesh.text = s;
    }
    public void Hide()
    {
        if (locked) return;
        gameObject.SetActive(false);
    }
}
