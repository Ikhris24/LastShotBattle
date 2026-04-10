using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button startGameButton;
    public Button creditsButton;
    public Button exitGameButton;

    [SerializeField] private TextMeshProUGUI creditText;

    private void Start()
    {
        creditText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        startGameButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(ShowCredits);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        startGameButton.onClick.RemoveListener(StartGame);
        creditsButton.onClick.RemoveListener(ShowCredits);
        exitGameButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("0");
    }

    private void ShowCredits()
    {
        StartCoroutine(Credits());
    }

    private void ExitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    //Play Credits Text for 5 Seconds. 
    IEnumerator Credits()
    {
        creditText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        creditText.gameObject.SetActive(false);
    }
}