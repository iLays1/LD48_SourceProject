using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEquipingCanvas : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] hiders;
    PlayerMobileController controller;
    WaitForSeconds startDelay = new WaitForSeconds(0.5f);

    private void Awake()
    {
        controller = FindObjectOfType<PlayerMobileController>();
    }

    private void Start()
    {
        Begin();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            End();
        }
    }

    public void Begin()
    {
        controller.isActive = false;
        panel.SetActive(true);
        foreach (var h in hiders)
            h.SetActive(false);
    }
    public void End()
    {
        panel.SetActive(false);
        foreach (var h in hiders)
            h.SetActive(true);

        TickManager.ActivateHazards.Invoke();

        StartCoroutine(WaitForMovement());
    }
    IEnumerator WaitForMovement()
    {
        yield return startDelay;

        controller.isActive = true;
    }
}
