using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI waveInfoText;
    [SerializeField] float waveDuration;
    [SerializeField] EnemyPool enemyPool;

    float correction;
    int curWave;

    public void GameStart()
    {
        correction = 1; // 나중에 스테이지 마다 보정 값 넣어줘야함
        curWave = 1;
        waveInfoText.text = "WAVE " + curWave;
        StartCoroutine(CoTimer());
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    IEnumerator CoTimer()
    {
        float waveTime = 0;

        while (true)
        {
            float sliderValue = Mathf.Lerp(0, 1, waveTime / waveDuration);
            timerSlider.value = sliderValue;

            yield return null;

            waveTime += Time.deltaTime;

            if (waveTime > waveDuration)
            {
                waveTime = 0;
                curWave++;
                correction += 0.3f;
                waveInfoText.text = "WAVE " + curWave;
                enemyPool.ChangeWave(curWave, correction);
            }
        }
    }
}
