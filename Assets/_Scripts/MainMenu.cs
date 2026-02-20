using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play Game
    public void PlayGame()
    {
        SceneManager.LoadScene(0); // SampleScene
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // Show Credits
    public GameObject creditsPanel;

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}