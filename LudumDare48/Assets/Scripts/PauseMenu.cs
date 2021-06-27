using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseContainer;
    PlayerMobileController playerController;
    bool paused = false;
    WaitForSeconds startDelay = new WaitForSeconds(0.5f);
    PlayerActionButton[] actionButtons;

    private void Start()
    {
        actionButtons = FindObjectsOfType<PlayerActionButton>();
        playerController = FindObjectOfType<PlayerMobileController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                UnPauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;

        playerController.isActive = false;
        foreach (var actionButton in actionButtons)
            actionButton.active = false;


        pauseContainer.SetActive(true);
        StopAllCoroutines();
    }

    public void UnPauseGame()
    {
        paused = false;
        pauseContainer.SetActive(false);

        StartCoroutine(WaitForMovement());
    }
    IEnumerator WaitForMovement()
    {
        yield return startDelay;

        playerController.isActive = true;
        foreach (var actionButton in actionButtons)
            actionButton.active = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
}
