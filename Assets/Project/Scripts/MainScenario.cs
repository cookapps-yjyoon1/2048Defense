using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScenario : MonoBehaviour
{
    [SerializeField] private Image imageMatrix;
    [SerializeField] private TextMeshProUGUI textMatrix;
    [SerializeField] private Sprite[] spritesMatrix;

    private int matrixIndex = 0;

    public void Start()
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
        if (matrixIndex == 0)
        {
            return;
        }
        
        SoundManager.Instance.Play(Enum_Sound.Effect, "MoveStage");
        matrixIndex = matrixIndex > 0 ? matrixIndex - 1 : spritesMatrix.Length - 1;

        imageMatrix.sprite = spritesMatrix[matrixIndex];
        textMatrix.text = spritesMatrix[matrixIndex].name;
    }

    public void OnClickRight()
    {
        if (matrixIndex == spritesMatrix.Length -1)
        {
            return;
        }

        SoundManager.Instance.Play(Enum_Sound.Effect, "MoveStage");
        matrixIndex = matrixIndex < spritesMatrix.Length - 1 ? matrixIndex + 1 : 0;

        imageMatrix.sprite = spritesMatrix[matrixIndex];
        textMatrix.text = spritesMatrix[matrixIndex].name;
    }
}