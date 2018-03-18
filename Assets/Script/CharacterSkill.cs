using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour {
    public int player1Character;
    public int player2Character;
    public PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        Skill1();
        Skill2();
    }
    public int Skill1()
    {
        if (PlayerPrefs.GetInt("Player1Character") == 1)
        {
            player1Character = 1;
        }
        else if (PlayerPrefs.GetInt("Player1Character") == 2)
        {
            player1Character = 2;
        }
        else
        {
            player1Character = 3;
        }
        return player1Character;
    }
    public int Skill2()
    {
        if (PlayerPrefs.GetInt("Player2Character") == 1)
        {
            player2Character = 1;
        }
        else if (PlayerPrefs.GetInt("Player2Character") == 2)
        {
            player2Character = 2;
        }
        else
        {
            player2Character = 3;
        }
        return player2Character;
    }
}