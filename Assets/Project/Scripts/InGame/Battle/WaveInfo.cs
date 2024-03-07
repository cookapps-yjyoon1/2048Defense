using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI waveInfoText;
    [SerializeField] float waveDuration;
    [SerializeField] EnemyPool enemyPool;
    //[SerializeField] WaveData curWaveData;

    public int CurWave { get; private set; }

    //public WaveData CurWaveData { get => curWaveData; private set => curWaveData = value; }
    //public float[] CurWaveEnemyProb { get; private set; }

    public void GameStart()
    {
        CurWave = 1;
        StartCoroutine(CoTimer());
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    IEnumerator CoTimer()
    {
        float waveTime = 0;

        //CurWaveData = GameManager.instance.dataMgr.GetWaveData(curWave);

        while (true)
        {
            float sliderValue = Mathf.Lerp(0, 1, waveTime / waveDuration);
            timerSlider.value = sliderValue;

            yield return null;

            waveTime += Time.deltaTime;

            if (waveTime > waveDuration)
            {
                waveTime = 0;
                CurWave++;
                waveInfoText.text = "WAVE " + CurWave;
                //CurWaveData = GameManager.instance.dataMgr.GetWaveData(curWave);
                //print("Wave : " + CurWaveData.wave + " / Spawn Time : " + CurWaveData.spawnTime + " / Spawn Count : " + CurWaveData.spawnCount + " / buff : " + CurWaveData.buff);
                enemyPool.ChangeWave(CurWave, 1);
            }
        }
    }
}
