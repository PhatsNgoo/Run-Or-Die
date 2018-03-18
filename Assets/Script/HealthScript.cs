using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour {
    public Button pauseButton;
    public Button resumeButton;
	public GameObject player1;
	public GameObject player2;
    public GameObject readyImage;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public AudioSource collideSound;
    public AudioSource jumpSound;
    public AudioSource runSound;
    public AudioSource Volume;
    public AudioSource trapSound;
    public AudioSource sfxAudioSource;
    public Button applySettingButton;
    public Button exitSettingButton;
    public Image settingBackground;
    public Slider soundSlider;

    public Image healthBarPlayer1;
	public Image healthBarPlayer2;
	public float player1HealthScale;
    public float player2HealthScale;
    private float x;
    private float y;
    private float z;
    public bool isPausing;
    private void Awake()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        isPausing = true;
        resumeButton.gameObject.SetActive(false);
        x = healthBarPlayer1.transform.localScale.x;
        y = healthBarPlayer1.transform.localScale.y;
        z = healthBarPlayer1.transform.localScale.z;
    }

    private void Start()
    {
        Volume.PlayDelayed(1);
        //Volume.PlayOneShot(startSound, 2f);
    }
    public void Pause()
    {
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(true);
        enabled = false;
        Time.timeScale = 0;
        isPausing = true;
        resumeButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
    public void Resume()
    {
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(false);
        enabled = true;
        Time.timeScale = 1f;
        isPausing = false;
        resumeButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
    public void SoundSetting()
    {
        Volume.volume = trapSound.volume = collideSound.volume = runSound.volume = jumpSound.volume = sfxAudioSource.volume = soundSlider.value;
    }
    public void applySetting()
    {
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        Resume();
    }
    public void cancelSetting()
    {
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(false);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        Resume();
    }
    void Update(){
        SoundSetting();
        if (Time.timeSinceLevelLoad < 1)
        {
            readyImage.gameObject.SetActive(true);
            three.gameObject.SetActive(false);
            two.gameObject.SetActive(false);
            one.gameObject.SetActive(false);
        }
        else if (Time.timeSinceLevelLoad >= 1 && Time.timeSinceLevelLoad < 2)
        {
            readyImage.gameObject.SetActive(false);
            three.gameObject.SetActive(true);
            two.gameObject.SetActive(false);
            one.gameObject.SetActive(false);
        }
        else if (Time.timeSinceLevelLoad >= 2 && Time.timeSinceLevelLoad < 3)
        {
            readyImage.gameObject.SetActive(false);
            three.gameObject.SetActive(false);
            two.gameObject.SetActive(true);
            one.gameObject.SetActive(false);
        }
        else if (Time.timeSinceLevelLoad >= 3 && Time.timeSinceLevelLoad < 4)
        {
            readyImage.gameObject.SetActive(false);
            three.gameObject.SetActive(false);
            two.gameObject.SetActive(false);
            one.gameObject.SetActive(true);
        }
        else if (Time.timeSinceLevelLoad >= 4)
        {
            one.gameObject.SetActive(false);
            isPausing = false;
        }
        if (player1.transform.position.x > player2.transform.position.x+178) {
			healthBarPlayer2.transform.localScale -= new Vector3(player2HealthScale,0,0);
		}
		if (player2.transform.position.x > player1.transform.position.x + 178) {
			healthBarPlayer1.transform.localScale -= new Vector3 (player1HealthScale, 0, 0);
		}
        if (healthBarPlayer1.transform.localScale.x > x)
        {
            healthBarPlayer1.transform.localScale = new Vector3(x, y, z);
        }
        if (healthBarPlayer2.transform.localScale.x > x)
        {
            healthBarPlayer2.transform.localScale = new Vector3(x, y, z);
        }
        if (healthBarPlayer2.transform.localScale.x <= 0) 
		{
            PlayerPrefs.SetInt("PlayerWin", 1);
            PlayerPrefs.SetInt("PlayerDie", 2);
            SceneManager.LoadScene("EndScene");
            healthBarPlayer2.transform.localScale = new Vector3(0, 0, 0);
			player1HealthScale = 0f;
            player2HealthScale = 0f;
        }
        else if (healthBarPlayer1.transform.localScale.x <= 0)
        {
            PlayerPrefs.SetInt("PlayerWin", 2);
            PlayerPrefs.SetInt("PlayerDie", 1);
            SceneManager.LoadScene("EndScene");
            healthBarPlayer1.transform.localScale = new Vector3(0, 0, 0);
            player1HealthScale = 0f;
            player2HealthScale = 0f;
        }
    }
}
