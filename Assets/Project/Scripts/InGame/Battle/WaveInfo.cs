using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI waveInfoText;
    [SerializeField] float waveDuration;
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform bigEnergySpawnPos;
    [SerializeField] GameObject btnStart;

    public int curWave;
    int lastWave = 4;
    float waveTime = 0;

    public void GameStart(int _stage)
    {
        curWave = 0;
        btnStart.SetActive(false);
        waveInfoText.text = "WAVE\n" + (curWave / 5 +1)+ " - " + (curWave % 5+1);
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }

    public void WaveStart()
    {
        GameManager.Instance.isStart = true;
        btnStart.SetActive(false);
        waveInfoText.text = "WAVE\n" + (curWave / 5 +1)+ " - " + (curWave % 5+1);
        StartCoroutine(CoTimer());
        enemyPool.ChangeWave(curWave);
    }

    public void WaveStop()
    {
        StopAllCoroutines();
    }

    public void WaveEnd(Vector3 _pos)
    {
        StopAllCoroutines();
        StartCoroutine(CoWaveEnd(_pos));
    }

    IEnumerator CoWaveEnd(Vector3 _pos)
    {

        for (int i = (int)(waveDuration - waveTime); i <= waveDuration; i++)
        {
            timerSlider.value = i;
            GameManager.Instance.obPool.SpawnEnergy(_pos, Random.Range(1, 3));
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.isStart = false;
        btnStart.gameObject.SetActive(true);

        curWave++;
        lastWave += 5;
        waveInfoText.text = "WAVE\n" + (curWave / 5 +1)+ " - " + (curWave % 5+1);
    }

    IEnumerator CoTimer()
    {
        waveTime = 0;

        while (true)
        {
            //float sliderValue = Mathf.Lerp(0, 1, waveTime / waveDuration);
            timerSlider.value = waveTime;

            yield return null;

            waveTime += Time.deltaTime;

            if (waveTime > waveDuration)
            {
                if (curWave == lastWave)
                {
                    yield break;
                }

                waveTime = 0;
                curWave++;
                waveInfoText.text = "WAVE\n" + (curWave / 5 +1)+ " - " + (curWave % 5+1);
                enemyPool.ChangeWave(curWave);
            }
        }
    }

    public void OnButtonClick()
    {
        WaveStart();
    }
}
