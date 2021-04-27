using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour
{
    public static bool[] levelsUnlocked = new bool[]
    {
        true, //burn
        true, //l1
        false,//l2
        false,//l3
        false //l4
    };

    public static void UnlockLevel(int i)
    {
        if (i > levelsUnlocked.Length - 1)
            return;

        levelsUnlocked[i] = true;
    }

    public Button[] levelButtonsInOrder;
    public Image[] lockImagesInOrder;

    private void Start()
    {
        for (int i = 0; i < levelsUnlocked.Length; i++)
        {
            if (i == 0 || i == 1)
                continue;

            if (levelsUnlocked[i])
            {
                levelButtonsInOrder[i - 2].interactable = true;
                lockImagesInOrder[i - 2].gameObject.SetActive(false);
            }
        }
    }
}
