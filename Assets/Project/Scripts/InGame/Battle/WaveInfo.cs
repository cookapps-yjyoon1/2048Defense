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
    //float incrCorrection;
    public int curWave;
    public bool lastTimer = false;

    public void GameStart(int _stage)
    {
        correction = GameManager.instance.stageData.stage[_stage].correction;  
        curWave = 0;
        waveInfoText.text = "WAVE " + curWave / 5 + " - " + curWave % 5;
        //StartCoroutine(CoTimer());
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    public void WaveStart()
    {
        StartCoroutine(CoTimer());
        enemyPool.ChangeWave(curWave, correction);
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
                //correction += 1;
                //waveInfoText.text = "WAVE " + curWave;
                waveInfoText.text = "WAVE " + ((curWave / 5) + 1) + " - " + curWave % 5;
                enemyPool.ChangeWave(curWave, correction);
                //enemyPool.WaveStop();
                
                if(lastTimer)
                {
                    curWave++;
                    yield break;
                }
            }
        }
    }
}
