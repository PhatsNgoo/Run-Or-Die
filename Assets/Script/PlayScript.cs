using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour {
    public List<GameObject> player1Prefabs;
    public List<GameObject> player2Prefabs;

    public GameObject player1;
    public GameObject player2;

    private int player1Character;
    private int player2Character;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Player1Character") == 1)
        {
            player1Character = 0;
        }
        else if (PlayerPrefs.GetInt("Player1Character") == 2)
        {
            player1Character = 1;

        }
        else
        {
            player1Character = 2;
        }
        if (PlayerPrefs.GetInt("Player2Character") == 1)
        {
            player2Character = 0;
        }
        else if (PlayerPrefs.GetInt("Player2Character") == 2)
        {
            player2Character = 1;
        }
        else
        {
            player2Character = 2;
        }
        
    }
    private void Start()
    {
        player1 = Instantiate(
                player1Prefabs[player1Character],
                new Vector3(-50, -10, 0),
                Quaternion.identity

            );
        player2 = Instantiate(
                player2Prefabs[player2Character],
                new Vector3(-80, -10, 0),
                Quaternion.identity
            );
    }
}
