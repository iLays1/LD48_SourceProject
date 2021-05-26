using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawnSequencer : MonoBehaviour
{
    public Image screenOverlay;
    FallingPlayer player;
    LevelManager levelManager;

    private void Awake()
    {
        player = FindObjectOfType<FallingPlayer>();
        levelManager = GetComponent<LevelManager>();    
    }

    public void SpawnBossSequence(int bossPos)
    {
        StartCoroutine(SpawnBossSequenceCoroutine(bossPos));
    }
    IEnumerator SpawnBossSequenceCoroutine(int bossPos)
    {
        //Fadeout music
        player.isActive = false;

        Sequence s = DOTween.Sequence();

        float flashTime = 2f;
        Color flashColor = Color.red * 0.4f;

        s.Append(screenOverlay.DOColor(flashColor, flashTime / 6));
        s.Append(screenOverlay.DOColor(Color.clear, flashTime / 6));
        s.Append(screenOverlay.DOColor(flashColor, flashTime / 6));
        s.Append(screenOverlay.DOColor(Color.clear, flashTime / 6));
        s.Append(screenOverlay.DOColor(flashColor, flashTime / 6));
        s.Append(screenOverlay.DOColor(Color.clear, flashTime / 6));
        
        yield return new WaitForSeconds(flashTime);

        levelManager.SpawnBoss(bossPos);
        //Display name text

        yield return new WaitForSeconds(0.3f);
        //start boss music up
        player.isActive = true;
    }
}
