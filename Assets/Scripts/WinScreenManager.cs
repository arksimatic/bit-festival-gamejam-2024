using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WinScreenManager : MonoBehaviour
{
    public GameObject Panel;
    public void OnShowWinScreen()
    {
        Panel.SetActive(true);
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
