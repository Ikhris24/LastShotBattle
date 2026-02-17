using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [Header("Text")]

    public Text roundCall;

    public Text knockOutText;

    public Text playerWinText;

    public Text perfectKnockOutText;

    [Header("Dynamic")]

    [SerializeField]private int _round;

    [SerializeField]private int _roundSet;

    [SerializeField]private bool _isPlayerWinning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
