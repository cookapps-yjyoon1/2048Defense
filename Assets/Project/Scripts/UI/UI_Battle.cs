using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Battle : MonoBehaviour
{
    [Header("Win")]
    [SerializeField] private GameObject WinPanel;
    
    [Header("Lose")]
    [SerializeField] private GameObject LosePanel;

    private int _wave;
    
    public void FinishGame(bool isWin,int wave)
    {
        _wave = wave;
        WinPanel.SetActive(isWin);
        LosePanel.SetActive(!isWin);

        gameObject.SetActive(true);
    }
    
    public void OnClickSceneLoaded()
    {
        if (WinPanel.activeInHierarchy)
        {
            PlayerDataManager.BoxData.TryAddBox(2);
        }
        else
        {
            if (_wave >= 29)
            {
                PlayerDataManager.BoxData.TryAddBox(1);
            }
            else if(_wave >= 14)
            {
                PlayerDataManager.BoxData.TryAddBox(0);
            }
        }

        SceneManager.LoadScene("01Main");
    }
}
