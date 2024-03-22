
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : SingletonBehaviour<TimeManager>
{
    private Action _onTick;
    private Coroutine _tickerCoroutine;
    public long Now;

    private void Start()
    {
        StartCoroutine(StartAllCoroutines());
    }

    private IEnumerator StartAllCoroutines()
    {
        yield return StartCoroutine(GetNetworkTime());
        
        StopCoroutineAndNull(ref _tickerCoroutine);
        _tickerCoroutine = StartCoroutine(TickCoroutine());
    }
    
    private void StopCoroutineAndNull(ref Coroutine coroutine)
    {
        if (null != coroutine)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator TickCoroutine()
    {
        // var questData = PlayerDataManager.QuestData;
        var second = new WaitForSecondsRealtime(1f);
        while (true)
        {
            yield return second;

            try
            {
                _onTick?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                
            }
        }
    }
    
    public void AddOnTickCallback(Action callback)
    {
        _onTick -= callback;
        _onTick += callback;
    }    
    
    public void RemoveOnTickCallback(Action callback)
    {
        _onTick -= callback;
    }

    private IEnumerator GetNetworkTime()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("www.google.com"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date"); //이곳에서 반송된 데이터에 시간 데이터가 존재

                var day = DateTime.Parse(date);
                Now = (day.Year - 2000) * 31536000 + day.Day * 86400 + day.Hour * 3600 + day.Minute * 60 + day.Second;
                
                AddOnTickCallback(() =>
                {
                    Now++;
                });
            }
        }
    }

}
