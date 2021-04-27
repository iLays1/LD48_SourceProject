using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int levelIndex;
    //a
    public void CLICKED()
    {
        StartCoroutine(levelTransCoroutine());
    }
    IEnumerator levelTransCoroutine()
    {
        CanvasManagerMenu.returnToCanvas = true;
        FindObjectOfType<Fader>().FadeOut(1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
