using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScenario : MonoBehaviour
{
    [SerializeField] private Text _txtStage;
    [SerializeField] private GameObject _goIsClear;
    [SerializeField] private GameObject _goLeft;
    [SerializeField] private GameObject _goRight;
    
    private int _curStageIndex = 0;

    public void OnEnable()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Lobby");
        Refresh();
    }

    public void Refresh()
    {
        var isClear = PlayerDataManager.ETCData.IsClearStage[_curStageIndex];
        
        _txtStage.text = $"{_curStageIndex+1}";
        _goIsClear.SetActive(isClear);
        
        _goLeft.SetActive(_curStageIndex > 0);
        _goRight.SetActive(_curStageIndex < PlayerDataManager.ETCData.IsClearStage.Count-1);
    }

    public void OnClickGameStart()
    {
        PlayerDataManager.ETCData.CurStage = _curStageIndex;
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