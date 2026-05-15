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
    public Health1 player1Health;
    public Health2 player2Health;

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
        StartCoroutine(HandleRoundEnd(winningPlayer));
    }

    IEnumerator HandleRoundEnd(int winningPlayer)
    {
        knockOutText.gameObject.SetActive(true);
        knockOutText.text = "K.O.";

        yield return new WaitForSeconds(2f);

        knockOutText.gameObject.SetActive(false);

        if (winningPlayer == 1)
        {
            _player1Wins++;
        }
        else if (winningPlayer == 2)
        {
            _player2Wins++;
        }

        // Check if match is over
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
            // Next round
            _round++;

            // Reset health
            player1Health.currentHealth = player1Health.maxHealth;
            player1Health.SendMessage("UpdateHealthBar");

            player2Health.currentHealth = player2Health.maxHealth;
            player2Health.SendMessage("UpdateHealthBar");

            roundOver = false;

            StartCoroutine(StartRound());w
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
        // Prevent multiple round endings
        if (roundOver)
            return;

        // Player 1 lost
        if (player1Health.currentHealth <= 0)
        {
            roundOver = true;
            EndRound(2);
        }

        // Player 2 lost
        else if (player2Health.currentHealth <= 0)
        {
            roundOver = true;
            EndRound(1);
        }

        // Debug Keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            roundOver = true;
            EndRound(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            roundOver = true;
            EndRound(2);
        }
    }

}