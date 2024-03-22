using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Battle : MonoBehaviour
{
    [Header("Win")]
    [SerializeField] private GameObject WinPanel;
    
    [Header("Lose")]
    [SerializeField] private GameObject LosePanel;

    public void FinishGame(bool isWin)
    {
        WinPanel.SetActive(isWin);
        LosePanel.SetActive(!isWin);

        gameObject.SetActive(true);
    }
    
    public void OnClickSceneLoaded()
    {
        if (WinPanel.activeInHierarchy)
        {
            PlayerDataManager.BoxData.TryAddBox();
        }

        SceneManager.LoadScene("01Main");
    }
}
