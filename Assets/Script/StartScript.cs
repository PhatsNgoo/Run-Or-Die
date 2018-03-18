using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {
	public Button startButton;
	public Button settingButton;
	public Button helpButton;
    public Button applySettingButton;
    public Button exitCreditsButton;
    public Button exitSettingButton;
    public Image settingBackground;
    public Image creditsBackground;
    public Slider soundSlider;
    public Slider musicSlider;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public InputField player1InputName;
    public InputField player2InputName;
    public Button player1Save;
    public Button player2Save;
    

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("MusicVolume") == null && PlayerPrefs.GetFloat("SoundVolume") == null)
        {
            musicSlider.value = 1;
            soundSlider.value = 1;
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        }
        if (PlayerPrefs.GetString("Player1Name")!=null && PlayerPrefs.GetString("Player2Name")!=null)
        {
            player1InputName.text = PlayerPrefs.GetString("Player1Name");
            player2InputName.text = PlayerPrefs.GetString("Player2Name");
        }
        else
        {
            player1InputName.text = "Player1";
            player2InputName.text = "Player2";
        }
    }
    private void Start()
    {
    }
    private void Update()
    {
        soundSetting();
        musicSetting();
        player1SaveName();
        player2SaveName();
    }
    public void start (){
        sfxAudioSource.Play();
        SceneManager.LoadScene("CharacterScene");
	}
    public void exitCredits()
    {
        sfxAudioSource.Play();
        creditsBackground.gameObject.SetActive(false);
    }
	public void setting (){
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(true);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        
	}
    public void soundSetting()
    {
        sfxAudioSource.volume = soundSlider.value;
    }
    public void musicSetting ()
    {
        musicAudioSource.volume = musicSlider.value;
    }
    public void applySetting()
    {
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("MusicVolume",musicSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }
    public void cancelSetting()
    {
        sfxAudioSource.Play();
        settingBackground.gameObject.SetActive(false);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }
    public void player2SaveName()
    {
        PlayerPrefs.SetString("Player2Name", player2InputName.text);
    }
    public void player1SaveName()
    {
        PlayerPrefs.SetString("Player1Name", player1InputName.text);
    }
    public void help (){
        sfxAudioSource.Play();
        creditsBackground.gameObject.SetActive(true);
    }
}
