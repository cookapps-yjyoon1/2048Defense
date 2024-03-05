using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    //[SerializeField] Slider timerSlider;
    //[SerializeField] TextMeshProUGUI waveInfoText;
    //[SerializeField] float waveDuration;
    //[SerializeField] WaveData curWaveData;

    //public WaveData CurWaveData { get => curWaveData; private set => curWaveData = value; }
    //public float[] CurWaveEnemyProb { get; private set; }
    //public bool IsSpawn { get; private set; }

    //public void GameStart()
    //{
    //    IsSpawn = true;
    //    StartCoroutine(CoTimer());
    //}

    //public void GameOver()
    //{
    //    StopAllCoroutines();
    //}

    //IEnumerator CoTimer()
    //{
    //    float waveTime = 0;
    //    int curWave = 0;

    //    CurWaveData = GameManager.instance.dataMgr.GetWaveData(curWave);

    //    while (true)
    //    {
    //        float sliderValue = Mathf.Lerp(0, 1, waveTime / waveDuration);
    //        timerSlider.value = sliderValue;

    //        yield return null;

    //        waveTime += Time.deltaTime;

    //        if (waveTime > waveDuration - 2)
    //        {
    //            IsSpawn = false;
    //        }

    //        if (waveTime > waveDuration)
    //        {
    //            waveTime = 0;
    //            curWave++;
    //            waveInfoText.text = "WAVE " + curWave;
    //            IsSpawn = true;
    //            CurWaveData = GameManager.instance.dataMgr.GetWaveData(curWave);
    //            //print("Wave : " + CurWaveData.wave + " / Spawn Time : " + CurWaveData.spawnTime + " / Spawn Count : " + CurWaveData.spawnCount + " / buff : " + CurWaveData.buff);
    //        }
    //    }
    //}
}
