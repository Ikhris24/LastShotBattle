using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int player1Character;
    public static int player2Character;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}