using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScene_Title : MonoBehaviour
    {
        #region public

        public void Start()
        {
            _lastProgress = 0f;
            _isStartLoading = true;
            StartCoroutine(Initialize());
            
            _trTitle.localScale = Vector3.zero;
        }

        public void OnClickButton()
        {
            SoundManager.Instance.Play(Enum_Sound.Effect, "Reload");
            SceneManager.LoadScene("01Main");
        }

        #endregion

        #region protected

        #endregion

        #region private

        [SerializeField] private Text _txtLoading;

        [SerializeField] private Slider _sliderLoading;

        [SerializeField] private GameObject _goPrgressBar;
        [SerializeField] private GameObject _goStart;
        [SerializeField] private Transform _trTitle;

        private readonly string[] loadingString = new string[]
        {
            "Make Zombies.",
            "Make Zombies..",
            "Make Zombies...",
            "Make Zombies.",
            "Make Zombies..",
            "Make Zombies...",
            "Make Zombies.",
            "Make Zombies..",
            "Make Zombies...",
            "Make Zombies.",
        };

        private bool _lock;

        private float _lastProgress;

        private bool _isStartLoading;

        private bool _isDone;

        private float Progress;

        private void UpdateProgress(float progress)
        {
            bool isDone = 1 <= progress;

            _goPrgressBar.SetActive(!isDone);
            _goStart.SetActive(isDone);

            if (isDone)
            {
                _txtLoading.text = "They are coming...";
            }
            else
            {
                _txtLoading.text = loadingString[(int)progress];
            }

            _sliderLoading.value = progress;
        }

        #endregion

        #region lifecycle

        protected void Awake()
        {
            _lock = true;
            _txtLoading.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            Application.targetFrameRate = 60;
        }

        protected void OnEnable()
        {
            _isStartLoading = false;
            _isDone = false;
        }

        private void Update()
        {
            if (!_isStartLoading)
            {
                return;
            }
            
            if (0.01f < Math.Abs(_lastProgress - Progress))
            {
                UpdateProgress(Progress);

                _lastProgress = Progress;
            }

            if (!(1.0f <= _lastProgress))
            {
                return;
            }

            _isStartLoading = false;
            _isDone = true;
            UpdateProgress(Progress);
        }

        #endregion
        
        private IEnumerator Initialize()
        {
            Progress = 0.1f;
            yield return null;

            SoundManager.Instance.Play(Enum_Sound.Bgm, "Loading");

            // //페이스북 초기화
            // if (!InitFaceBookSDk())
            // {
            //     return;
            // }

            Progress = 0.2f;
            yield return null;

            _trTitle.DOScale(new Vector3(1.2f, 1.2f, 1), 0.4f).OnComplete(() =>
                    _trTitle.DOScale(new Vector3(1f, 1f, 1), 0.2f).SetEase(Ease.OutBack))
                .SetEase(Ease.OutBack);
            
            Progress = 0.3f;
            yield return null;

            Progress = 0.4f;
            yield return null;

            Progress = 0.5f;
            yield return null;
            
            Progress = 0.6f;
            yield return null;
            
            yield return null;

            //유저 로컬 데이터 로드 & 초기화
            yield return StartCoroutine(PlayerDataManager.Instance.LoadLocalData());
            Progress = 0.8f;
            yield return null;

            // 유저 로컬 데이터 저장
            yield return StartCoroutine(PlayerDataManager.Instance.SaveLocalData());
            Progress = 0.9f;
            yield return null;

            yield return StartCoroutine(PlayerDataManager.Instance.ValidCheck());
            Progress = 1f;
            yield return null;

            Debug.Log("로딩 완료 게임 진입 가능");
        }
    }

