using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("FMOD")]
    [SerializeField] private EventReference musicEvent;

    private EventInstance musicInstance;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        InitializeMusic();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializeMusic()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);

        musicInstance.start();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusicForScene(scene.name);
    }

    private void UpdateMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Menu":
                SetMusicState(0);
                break;

            case "0 (1)":
                SetMusicState(1);
                break;

            case "N/a":
                SetMusicState(2);
                break;

            case "n?A":
                SetMusicState(3);
                break;

            default:
                SetMusicState(0);
                break;
        }
    }

    public void SetMusicState(float value)
    {
        musicInstance.setParameterByName("SceneMusic", value);
        
    }

    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
   }
