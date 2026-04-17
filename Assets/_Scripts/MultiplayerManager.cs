using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using System.Collections;

public class MultiplayerManager : MonoBehaviour
{
    //Singleton
    public MultiplayerManager instance { get; private set; }

    [Header("Players")]
    public Movement playerOne;
    public Movement playerTwo;

    //These are for disabling players movement before round starts.
    private PlayerInput playerOneInput;
    private PlayerInput playerTwoInput;

    [Header("Spawn Locations")]
    public Transform[] spawnLocations;
    private int spawnLocIndex = 0;

    [Header("Health Bars")]
    public Image playerOneHealth;
    public Image playerTwoHealth;

    [Header("Player's Joined Text")]                //These will change when players press on controller, allowing them to "join" 
    public TextMeshProUGUI playerOneJoinedText;
    public TextMeshProUGUI playerTwoJoinedText;

    [Header("UI")]
    public TextMeshProUGUI countDownText;

    private void Awake()
    {
        instance = this;
        countDownText.text = "";
    }

    //Set Player's one and two to spawn on the left and right of the arena.
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.position = spawnLocations[spawnLocIndex].position;

        //Set both players to gameObject references. 
        if(spawnLocIndex == 0) 
        {
            playerOne = playerInput.GetComponent<Movement>();

            //Assign the Input Var to corresponding player, then disable movement. 
            playerOneInput = playerOne.GetComponent<PlayerInput>();
            playerOneInput.DeactivateInput();

            //Change text on screen to show player 1 joined
            ChangePlayerJoinedText("Player 1", playerOneJoinedText);
        }

        //After player two is spawned, flip them correctly, change text on screen, and then init the health bars. 
        if (spawnLocIndex == 1) 
        { 
            playerTwo = playerInput.GetComponent<Movement>();

            //Assign the Input Var to corresponding player, then disable movement. 
            playerTwoInput = playerTwo.GetComponent<PlayerInput>();
            playerTwoInput.DeactivateInput();

            playerTwo.FlipSprite(true);

            ChangePlayerJoinedText("Player 2", playerTwoJoinedText);

            //Then init health bars
            InitHealthBars();

            //Then start round
            StartCoroutine(StartRound());
        }

        spawnLocIndex++;
    }

    public void InitHealthBars()
    {
        playerOne.GetComponent<Health>().healthBarFill = playerOneHealth;

        playerTwo.GetComponent<Health>().healthBarFill = playerTwoHealth;

        //Because this function runs after both are spawned I will also have it recognize each other
        playerOne.FindOtherObject();
        playerTwo.FindOtherObject();
    }

    private void ChangePlayerJoinedText(string playerName, TextMeshProUGUI text)
    {
        text.text = $"{playerName} has joined!";
    }


    IEnumerator StartRound()
    {
        //Disable "Player has joined" text
        playerOneJoinedText.gameObject.SetActive(false);
        playerTwoJoinedText.gameObject.SetActive(false);

        //Countdown from 5 while also showing timer on screen. 
        for (int i = 5; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        //Show "Go!"
        countDownText.text = "Go!";

        yield return new WaitForSeconds(1f);

        //Then disable "Go!" text making it empty string. This is due to the same text object being used as the match timer. 
        countDownText.text = "";

        //Allow players to move.
        playerOneInput.ActivateInput();
        playerTwoInput.ActivateInput();

    }

}
