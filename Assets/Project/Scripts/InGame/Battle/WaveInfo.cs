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

    public float correction;
    float incrCorrection;
    public int curWave;

    public void GameStart(int _stage)
    {
        incrCorrection = GameManager.instance.stageData.stage[_stage].correction;
        correction = incrCorrection;
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
                correction += incrCorrection;
                waveInfoText.text = "WAVE " + curWave;
                enemyPool.ChangeWave(curWave, correction);
            }
        }
    }
}
