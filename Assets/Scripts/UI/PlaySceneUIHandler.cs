using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject youWinText;
    [SerializeField] private GameObject youLoseText;
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void DisplayGameoverScreen(bool isWin)
    {
        gameOverScreen.SetActive(true);
        if (isWin)
        {
            youWinText.SetActive(true);
        }
        else
        {
            youLoseText.SetActive(true);
        }
    }
}
