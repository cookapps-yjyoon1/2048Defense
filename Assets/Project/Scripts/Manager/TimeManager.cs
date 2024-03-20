
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
        Now = DateTime.Now.ToBinary();
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
                Now = DateTime.Parse(date).ToLocalTime().ToBinary();
                
                AddOnTickCallback(() =>
                {
                    Now++;
                });
            }
        }
    }

}
