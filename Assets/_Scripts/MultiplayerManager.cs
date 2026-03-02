using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour
{
    //Singleton
    public MultiplayerManager instance { get; private set; }

    [Header("Players")]
    public Movement playerOne;
    public Movement playerTwo;

    [Header("Spawn Locations")]
    public Transform[] spawnLocations;
    private int spawnLocIndex = 0;

    [Header("Health Bars")]
    public Image playerOneHealth;
    public Image playerTwoHealth;

    [Header("Player's Joined Text")]                //These will change when players press on controller, allowing them to "join" 
    public TextMeshProUGUI playerOneJoinedText;
    public TextMeshProUGUI playerTwoJoinedText;

    private void Awake()
    {
        instance = this;
    }

    //Set Player's one and two to spawn on the left and right of the arena.
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.position = spawnLocations[spawnLocIndex].position;

        //Set both players to gameObject references. 
        if(spawnLocIndex == 0) 
        {
            playerOne = playerInput.GetComponent<Movement>();

            //Change text on screen to show player 1 joined
            ChangePlayerJoinedText("Player 1", playerOneJoinedText);
        }

        //After player two is spawned, flip them correctly, change text on screen, and then init the health bars. 
        if (spawnLocIndex == 1) 
        { 
            playerTwo = playerInput.GetComponent<Movement>();
            playerTwo.FlipSprite(true);

            ChangePlayerJoinedText("Player 2", playerTwoJoinedText);

            //Then init health bars
            InitHealthBars(); 
        }

        spawnLocIndex++;
    }

    public void InitHealthBars()
    {
        playerOne.GetComponent<Health>().healthBarFill = playerOneHealth;

        playerTwo.GetComponent<Health>().healthBarFill = playerTwoHealth; 
    }

    private void ChangePlayerJoinedText(string playerName, TextMeshProUGUI text)
    {
        text.text = $"{playerName} has joined!";
    }

}
