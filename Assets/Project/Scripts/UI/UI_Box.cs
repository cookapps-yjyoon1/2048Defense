using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Box : MonoBehaviour
{
    [SerializeField] private Button _btnBoxOpen;
    [SerializeField] private GameObject _imgOpen;
    [SerializeField] private GameObject _imgLock;
    [SerializeField] private GameObject[] _boxList;
    [SerializeField] private Animator Animator;

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
        
        _imgOpen.gameObject.SetActive(isEnableClaim);

        if (isEnableClaim)
        {
            Animator.Play("WaitClaim");
        }
        else if (box.IsProgress)
        {
            Animator.Play("Progress");
        }
        else
        {
            Animator.Play("Idle");
        }
    }

    private void Awake()
    {
        _btnBoxOpen.onClick.AddListener(OpenBox);
    }

    private void OpenBox()
    {
        if (PlayerDataManager.BoxData.TryClaimBox(_index))
        {
            Refresh();
            return;
        }

        if (PlayerDataManager.BoxData.TryStartBox(_index))
        {
            Refresh();
            return;
        }
    }
}