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

    [SerializeField] private GameObject GameOverCutscene;

    public delegate void OnGameStart();

    public static event OnGameStart OnGameStartEvent;

    private int hours;
    private int minutes;
    private float seconds;
    private bool isMenuOpen;

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

    private void Start()
    {
        isGamePaused = true;
        CameraFade fade = Camera.main.GetComponent<CameraFade>();
        fade.ActivateFade();
        fade.OnFadeCompleteCallback += StartGame;

    }

    private void StartGame()
    {
        Camera.main.GetComponent<CameraFade>().OnFadeCompleteCallback-=StartGame;
        OnGameStartEvent?.Invoke();
        isGamePaused = false;
    }

    private void ToggleMenu()
    {
        if(isMenuOpen)
            CloseGameUI();
        else
        {
            OpenGameUI();
        }

        isMenuOpen = !isMenuOpen;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
        
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

    public void GameOver()
    {
        isGamePaused = true;
        GameOverCutscene.SetActive(true);
    }

    public void CloseGameUI()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        
        gameUI.SetActive(false);
    }

    public void ToggleTimer()
    {
        if(timerText.gameObject.activeSelf)
            timerText.gameObject.SetActive(false);
        else
        {
            timerText.gameObject.SetActive(true);
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void RestartGame()
    {
        instance = null;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
