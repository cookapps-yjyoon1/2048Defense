using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScenario : MonoBehaviour
{
    [SerializeField] private Text _txtStage;
    [SerializeField] private GameObject _goIsClear;
    [SerializeField] private GameObject _goLeft;
    [SerializeField] private GameObject _goRight;
    
    [SerializeField] private Image _imageStartBtn;
    [SerializeField] private Text _txtStartBtn;


    [SerializeField] private GameObject _goHard;
    [SerializeField] private GameObject _goIsClearHard;

    private int _curStageIndex = 0;
    private bool _curIsHard;

    public void OnEnable()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Lobby");
        Refresh();
    }

    public void Refresh()
    {
        var isClear = PlayerDataManager.ETCData.IsClearStage[_curStageIndex];
        var isHardClear = PlayerDataManager.ETCData.IsClearStageHard[_curStageIndex];

        _txtStage.text = $"{_curStageIndex+1}";
        _goIsClear.SetActive(isClear);
        _goHard.SetActive(isClear) ;
        
        _goLeft.SetActive(_curStageIndex > 0);
        _goRight.SetActive(_curStageIndex < PlayerDataManager.ETCData.IsClearStage.Count-1);

        _goIsClearHard.SetActive(isHardClear);
        _curIsHard = isClear;

        if (isHardClear)
        {
            _txtStartBtn.text = "Easy~";
            _imageStartBtn.color = new Color(1, 1, 1);
        }
        else
        {
            if (isClear)
            {
                _txtStartBtn.text = "Don't Start";
                _imageStartBtn.color = new Color(1, 0, 0);
            }
            else
            {
                _txtStartBtn.text = "Game Start";
                _imageStartBtn.color = new Color(1, 1, 1);
            }
        }
    }

    public void OnClickGameStart()
    {
        PlayerDataManager.ETCData.CurStage = _curStageIndex;
        PlayerDataManager.ETCData.IsHardMode = _curIsHard;

        SceneManager.LoadScene("02Game");
    }
    
    public void OnClickLeft()
    {
        if (_curStageIndex <= 0)
        {
            return;
        }
        
        _curStageIndex--;

        Refresh();
        
        
        SoundManager.Instance.Play(Enum_Sound.Effect, "MoveStage");
    }

    public void OnClickRight()
    {
        if (_curStageIndex >= PlayerDataManager.ETCData.IsClearStage.Count-1)
        {
            return;
        }

        _curStageIndex++;

        Refresh();
        
        SoundManager.Instance.Play(Enum_Sound.Effect, "MoveStage");
    }
}