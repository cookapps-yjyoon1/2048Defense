using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScenario : MonoBehaviour
{
    private int matrixIndex = 0;

    public void OnEnable()
    {
        SoundManager.Instance.Play(Enum_Sound.Bgm, "Lobby");
    }

    public void OnClickGameStart()
    {
        SceneManager.LoadScene("02Game");
    }

    public void OnClickGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
    }

    public void OnClickLeft()
    {
        
        SoundManager.Instance.Play(Enum_Sound.Effect, "MoveStage");

    }

    public void OnClickRight()
    {

        SoundManager.Instance.Play(Enum_Sound.Effect, "MoveStage");
    }
}