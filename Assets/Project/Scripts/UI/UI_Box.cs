using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Box : MonoBehaviour
{
    [SerializeField] private Button _btnBoxOpen;
    [SerializeField] private GameObject _imgOpen;
    [SerializeField] private GameObject _imgLock;

    private int _index = 0;


    private BoxData _data => PlayerDataManager.BoxData;

    public void Init(int index)
    {
        _index = index;

    }

    private void Awake()
    {
        _btnBoxOpen.onClick.AddListener(OpenBox);
        TimeManager.Instance.AddOnTickCallback(CanOpen);
    }

    private void OnEnable()
    {
    }

    private void OpenBox()
    {
        if (PlayerDataManager.BoxData.TryClaimBox(_index))
        {
            CloseBox();

        }

        if (PlayerDataManager.BoxData.TryStartBox(_index))
        {
            //TimeManager.Instance.AddOnTickCallback(CanOpen);
            _imgOpen.SetActive(true);
            //_imgLock.SetActive(true);
            // 색깔
        }
    }

    private void CloseBox()
    {
        _imgOpen.SetActive(false);
    }

    private void CanOpen()
    {
        if (!gameObject.activeSelf) { return;}

        if (_data.IsEnableStartBox())
        {
            _imgLock.SetActive(!_data.IsEnableStartBox());
        }
        else
        {
            _imgLock.SetActive(!_data.IsEnableClaimBox(_index));

        }

        if (_data.IsEnableClaimBox(_index))
        {
            //TimeManager.Instance.RemoveOnTickCallback(CanOpen);
        }
    }
}