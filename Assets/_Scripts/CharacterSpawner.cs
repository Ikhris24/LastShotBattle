using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;

    public Transform player1Spawn;
    public Transform player2Spawn;

    void Start()
    {
        Instantiate(characterPrefabs[GameManager.player1Character], player1Spawn.position, Quaternion.identity);
        Instantiate(characterPrefabs[GameManager.player2Character], player2Spawn.position, Quaternion.identity);
    }
}