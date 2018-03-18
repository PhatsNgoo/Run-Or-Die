using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterScript : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public AudioSource chooseSound;

    private float pos1;
    private float pos2;
    private bool chosing1;
    private bool chosing2;
    private void Awake()
    {
        if (PlayerPrefs.GetFloat("SoundVolume") == null)
            chooseSound.volume = PlayerPrefs.GetFloat("SoundVolume");
    }
    private void Start()
    {

        pos1 = player1.transform.position.x;
        pos2 = player2.transform.position.x;
        chosing1 = true;
        chosing2 = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            chooseSound.Play();
            pos1 = player1.transform.position.x;
            chosing1 = false;
            if (player1.transform.position.x == -100)
            {
                PlayerPrefs.SetInt("Player1Character", 1);
            }
            else if (player1.transform.position.x == 0)
            {
                PlayerPrefs.SetInt("Player1Character", 2);
            }
            else {
                PlayerPrefs.SetInt("Player1Character", 3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            chooseSound.Play();
            pos2 = player2.transform.position.x;
            chosing2 = false;
            if (player2.transform.position.x == -100)
            {
                PlayerPrefs.SetInt("Player2Character", 1);
            }
            else if (player2.transform.position.x == 0)
            {
                PlayerPrefs.SetInt("Player2Character", 2);
            }
            else
            {
                PlayerPrefs.SetInt("Player2Character", 3);
            }
        }
        if (!chosing1 && !chosing2)
        {
            SceneManager.LoadScene("PlayScene");
        }
        chooseCharacter1();
        chooseCharacter2();
        player1.transform.position = new Vector3(pos1, 40, 0);
        player2.transform.position = new Vector3(pos2, 40, 0);

        
    }
    private void chooseCharacter1()
    {
        if (chosing1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pos1 += 100;
            }
            if (player1.transform.position.x == 100 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                pos1 = -100;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pos1 -= 100;
            }
            if (player1.transform.position.x == -100 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pos1 = 100;
            }
        }
    }
    private void chooseCharacter2()
    {
        if (chosing2)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                pos2 += 100;
            }
            if (player2.transform.position.x == 100 && Input.GetKeyDown(KeyCode.D))
            {
                pos2 = -100;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                pos2 -= 100;
            }
            if (player2.transform.position.x == -100 && Input.GetKeyDown(KeyCode.A))
            {
                pos2 = 100;
            }
        }
    }
}
