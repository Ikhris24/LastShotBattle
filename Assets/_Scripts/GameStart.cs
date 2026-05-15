using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class GameStart : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text roundCall;
    public TMP_Text knockOutText;
    public TMP_Text playerWinText;
    public TMP_Text perfectKnockOutText;

    [Header("Dynamic")]
    [SerializeField] private int _round = 1;
    [SerializeField] private int _roundSet = 2; // First to 2 wins

    [SerializeField] private int _player1Wins = 0;
    [SerializeField] private int _player2Wins = 0;

    [Header("Player Health")]
    public Health player1Health;
    public Health player2Health;

    private bool roundOver = false;

    [Header("FMOD Events")]
    [SerializeField] private EventReference roundAnnounceEvent;
    [SerializeField] private EventReference fightAnnounceEvent;
    [SerializeField] private EventReference playerWinEvent;

    void Start()
    {
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        roundCall.gameObject.SetActive(true);

        roundCall.text = "ROUND " + _round;

        RuntimeManager.PlayOneShot(roundAnnounceEvent);

        yield return new WaitForSeconds(2f);

        roundCall.text = "FIGHT!";

        RuntimeManager.PlayOneShot(fightAnnounceEvent);

        yield return new WaitForSeconds(1f);

        roundCall.gameObject.SetActive(false);
    }

    // Call this when a player wins a round
    public void EndRound(int winningPlayer)
    {
        // Prevent multiple round endings
        if (roundOver)
            return;

        roundOver = true;

        StartCoroutine(HandleRoundEnd(winningPlayer));
    }

    IEnumerator HandleRoundEnd(int winningPlayer)
    {
        knockOutText.gameObject.SetActive(true);
        knockOutText.text = "K.O.";

        yield return new WaitForSeconds(2f);

        knockOutText.gameObject.SetActive(false);

        // Add win to correct player
        if (winningPlayer == 1)
        {
            _player1Wins++;
        }
        else if (winningPlayer == 2)
        {
            _player2Wins++;
        }

        // Match Over
        if (_player1Wins >= _roundSet)
        {
            ShowWinner(1);
        }
        else if (_player2Wins >= _roundSet)
        {
            ShowWinner(2);
        }
        else
        {
            // Next Round
            _round++;

            // Reset player health
            player1Health.ResetHealth();
            player2Health.ResetHealth();

            yield return new WaitForSeconds(1f);

            roundOver = false;

            StartCoroutine(StartRound());
        }
    }

    void ShowWinner(int player)
    {
        playerWinText.gameObject.SetActive(true);

        playerWinText.text = "PLAYER " + player + " WINS!";

        RuntimeManager.PlayOneShot(playerWinEvent);
    }

    void Update()
    {
        // Debug Keys

        // Player 1 wins round
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EndRound(1);
        }

        // Player 2 wins round
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EndRound(2);
        }
    }
}