using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndingScene : MonoBehaviour {
    public Button restartButton;
    public Button homeButton;
    public Image player1Win;
    public Image player2Win;
    public AudioSource endingSound;
    public AudioSource sfx;
    private void Awake()
    {
        if (PlayerPrefs.GetFloat("SoundVolume") == null)
            endingSound.volume= sfx.volume = PlayerPrefs.GetFloat("SoundVolume");
    }
    private void Start()
    {
        endingSound.Play();
        Debug.Log("player " + PlayerPrefs.GetInt("PlayerDie") + " die");
        Debug.Log("player " + PlayerPrefs.GetInt("PlayerWin") + " win");
        if (PlayerPrefs.GetInt("PlayerWin") == 1) { player1Win.gameObject.SetActive(true); }
        if (PlayerPrefs.GetInt("PlayerWin") == 2) { player2Win.gameObject.SetActive(true); }
    }
    public void Restart()
    {
        sfx.Play();
        SceneManager.LoadScene("CharacterScene");
    }
    public void Home()
    {
        sfx.Play();
        SceneManager.LoadScene("StartScene");
    }
}
