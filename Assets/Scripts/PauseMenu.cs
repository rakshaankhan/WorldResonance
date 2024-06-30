using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip select_clip;

    [SerializeField]
    private AudioClip back_clip;

    [SerializeField]
    private GameEvent sceneIsChaning;

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject InGameDebugObject;

    private bool isPaused = false;

    public delegate void TogglePause();
    public static TogglePause togglePause;
    private static PauseMenu instance;

    [SerializeField]
    private InputActionAsset playerActions;
    private InputActionMap playerActionMap;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        pauseMenuUI.SetActive(false);

        playerActionMap = playerActions.FindActionMap("Player");

    }
    private void Start()
    {
        InGameDebugObject.SetActive(false);
    }

    private void OnEnable()
    {
        togglePause += HandlePause;
    }
    private void OnDisable()
    {
        togglePause -= HandlePause;
    }
    public void HandlePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        playerActionMap.Enable();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerActionMap.Disable();
        Time.timeScale = 0f; // This will pause the game
        isPaused = true;
    }


    public void LoadLevelWithSceneField(SceneField level)
    {
        Resume();
        sceneIsChaning.TriggerEvent();
        SceneManager.LoadScene(level);
    }
    public void LoadLevel(int level)
    {
        Resume();
        sceneIsChaning.TriggerEvent();
        MusicManager.instance?.FadeOUT();
        SceneManager.LoadScene(level);
    }

    public void LoadMenu()
    {

        Resume();
        sceneIsChaning.TriggerEvent();
        SceneManager.LoadScene(0);

    }
    public void QuitGame()
    {
        // You can add more logic here like confirmation dialog if needed
        Application.Quit();
    }


    public void PlaySelectClip()
    {
        MusicManager.instance.PlayOneShotClip(select_clip);
    }

    public void PlayBackClip()
    {
        MusicManager.instance.PlayOneShotClip(back_clip);
    }

}