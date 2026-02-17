using UnityEngine;
using TMPro;

public class MatchTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("How long the match lasts (seconds)")]
    public float matchDuration = 99f;

    private float timeRemaining;
    private bool matchOver = false;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text timeOverText;

    void Start()
    {
        timeRemaining = matchDuration;

        if (timeOverText != null)
            timeOverText.gameObject.SetActive(false);

        UpdateTimerDisplay();
    }

    void Update()
    {
        if (matchOver)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
            CheckLowTimeWarning();
        }
        else
        {
            timeRemaining = 0;
            EndMatch();
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }

    void CheckLowTimeWarning()
    {
        if (timeRemaining <= 10f)
        {
            float flash = Mathf.Abs(Mathf.Sin(Time.time * 8f));
            timerText.alpha = Mathf.Lerp(0.3f, 1f, flash);
        }
        else
        {
            timerText.alpha = 1f;
        }
    }

    void EndMatch()
    {
        matchOver = true;

        if (timeOverText != null)
            timeOverText.gameObject.SetActive(true);

        Debug.Log("TIME OVER");
        Time.timeScale = 0f;
    }

    // Optional: call this for rematches or new rounds
    public void ResetTimer()
    {
        Time.timeScale = 1f;
        matchOver = false;
        timeRemaining = matchDuration;

        if (timeOverText != null)
            timeOverText.gameObject.SetActive(false);

        UpdateTimerDisplay();
    }
}
