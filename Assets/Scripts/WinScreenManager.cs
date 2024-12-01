using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public GameObject Panel;
    public TMP_Text text;
    public void OnShowWinScreen(string playerName)
    {
        Panel.SetActive(true);
        text.text = $"{playerName} wyszedł z pętli!";
    }

    public void OnRestartGame()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}