using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance=>instance;
    
    private static bool isGamePaused =false;
    public bool IsGamePaused => isGamePaused;
    
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private GameObject gameUI;

    [SerializeField] private TextMeshProUGUI timerText;

    private int hours;
    private int minutes;
    private float seconds;

    private void Awake()
    {
        if(instance!=null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
        
        SetVolume();
    }

    private void Update()
    {
        if(isGamePaused)
            return;

        seconds += Time.deltaTime;
        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }
        if(minutes>=60)
        {
            minutes = 0;
            hours++;
        }

        timerText.text = String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void SetVolume()
    {
        int volume = PlayerPrefs.GetInt("Volume",0);
        mixer.SetFloat("MasterVolume", Mathf.Clamp(volume, -80, 0));
    }
    
    public void OpenGameUI()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        
        gameUI.SetActive(true);
    }

    public void CloseGameUI()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        
        gameUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
