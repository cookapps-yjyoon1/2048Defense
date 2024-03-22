using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI waveInfoText;
    [SerializeField] float waveDuration;
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform bigEnergySpawnPos;

    //public float correction;
    //float incrCorrection;
    public int curWave;
    int lastWave = 4;
    float waveTime = 0;

    public void GameStart(int _stage)
    {
        //correction = GameManager.instance.stageData.stage[_stage].correction;  
        curWave = 0;
        waveInfoText.text = "WAVE\n" + curWave / 5 + " - " + curWave % 5;
        //StartCoroutine(CoTimer());
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    public void WaveStart()
    {
        waveInfoText.text = "WAVE\n" + curWave / 5 + " - " + curWave % 5;
        StartCoroutine(CoTimer());
        enemyPool.ChangeWave(curWave);
    }

    public void WaveStop()
    {
        StopAllCoroutines();
    }

    IEnumerator CoTimer()
    {
        waveTime = 0;

        while (true)
        {
            float sliderValue = Mathf.Lerp(0, 1, waveTime / waveDuration);
            timerSlider.value = sliderValue;

            yield return null;

            waveTime += Time.deltaTime;

            if (waveTime > waveDuration)
            {
                if (curWave == lastWave)
                {
                    curWave++;
                    lastWave += 5;
                    waveInfoText.text = "WAVE\n" + curWave / 5 + " - " + curWave % 5;
                    GameManager.instance.isStart = false;
                    GameManager.instance.obPool.SpawnEnergy(bigEnergySpawnPos.position, 30);


                    GameManager.instance.MoveEnergyFull(30);
                    yield break;
                }
                waveTime = 0;
                curWave++;
                waveInfoText.text = "WAVE\n" + curWave / 5 + " - " + curWave % 5;
                enemyPool.ChangeWave(curWave);
            }
        }
    }
}
