using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    private bool playerOneTurn = true;

    public void SelectCharacter(int characterIndex)
    {
        if (playerOneTurn)
        {
            GameManager.player1Character = characterIndex;
            playerOneTurn = false;
            Debug.Log("Player 1 selected character " + characterIndex);
        }
        else
        {
            GameManager.player2Character = characterIndex;
            Debug.Log("Player 2 selected character " + characterIndex);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("0 (1)");
    }
}