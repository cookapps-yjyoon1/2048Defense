using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Box : MonoBehaviour
{
    [SerializeField] private GameObject _imgOpen;
    [SerializeField] private GameObject _imgLock;
    [SerializeField] private GameObject[] _boxList;
    [SerializeField] private GameObject _goAds;
    [SerializeField] private Animator Animator;
    [SerializeField] private Text _remainTime;

    private int _index = 0;
    private BoxData _data => PlayerDataManager.BoxData;

    public void Init(int index)
    {
        _index = index;
    }

    public void Refresh()
    {
        var box = PlayerDataManager.BoxData.Boxes[_index];

        if (box.Level < 0)
        {
            gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < _boxList.Length; i++)
        {
            _boxList[i].SetActive(box.Level == i);
        }
        
        var isEnableClaim = PlayerDataManager.BoxData.IsEnableClaimBox(_index);
        
        if (isEnableClaim)
        {
            Animator.Play("WaitClaim");
            _remainTime.text = "Claim !";
        }
        else if (box.IsProgress)
        {
            Animator.Play("Progress");
            _remainTime.text = UtilCode.GetTimeFormat(PlayerDataManager.BoxData.FinishBoxTime - TimeManager.Instance.Now);
        }
        else
        {
            Animator.Play("Idle");
            _remainTime.text = UtilCode.GetTimeFormat(PlayerDataManager.BoxData.BoxTimes[box.Level]);
        }
        
        _imgOpen.gameObject.SetActive(isEnableClaim);
        
        _goAds.SetActive(PlayerDataManager.BoxData.IsEnableShowAds(_index));
        
        gameObject.SetActive(true);
    }
    
    public void OpenBox()
    {
        SoundManager.Instance.Play(Enum_Sound.Effect, "BoxClick");
        if (PlayerDataManager.BoxData.TryClaimBox(_index,out var list))
        {
            UI_Toast.Instance.Open(list);
            Refresh();
            return;
        }

        if (PlayerDataManager.BoxData.TryStartBox(_index))
        {
            Refresh();
            return;
        }
    }

    public void ShowAds()
    {
        if (!PlayerDataManager.BoxData.IsEnableShowAds(_index))
        {
            return;
        }
        
        CookApps.Admob.CAppAdmob.Rewarded.Show((isSuccess) =>
        {
            if (isSuccess)
            {
                PlayerDataManager.BoxData.Boxes[_index].IsShowAds = true;
                PlayerDataManager.BoxData.FinishBoxTime -= 1800;
                Refresh();
            }
        });
    }
}