using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{

    [SerializeField]
    private string path;
    [SerializeField]
    private string fileName;

    private PersistentGameData gameData;

    private List<IDataSaveLoad> objectsWithSaveLoad;

    private DatatoFileSystemManager fileSystem;

    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += AfterSceneLoad;
            fileSystem = new DatatoFileSystemManager(path, fileName);
            LoadFromFile();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AfterSceneLoad;
    }

    private void Start()
    {

    }



    public void LoadGame()
    {
        foreach (var persistenObject in objectsWithSaveLoad)
        {
            persistenObject.Load(gameData);
        }

    }

    [ContextMenu("SaveGameState")]
    public void SaveGame()
    {
        gameData.lastSceneOpen = SceneManager.GetActiveScene().name;
        gameData.NewGame = false;
        foreach (var persistenObject in objectsWithSaveLoad)
        {
            persistenObject.Save(gameData);
        }
    }

    public void FindAllPersistentObjectsInScene()
    {
        IEnumerable<IDataSaveLoad> objectsWithSaveLoadTemp = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaveLoad>();
        objectsWithSaveLoad = new List<IDataSaveLoad>(objectsWithSaveLoadTemp);

    }

    public void SaveToFile()
    {
        fileSystem.Save(gameData);
    }

    public void LoadFromFile()
    {
        gameData = fileSystem.Load();
    }


    private void AfterSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        FindAllPersistentObjectsInScene();
        LoadGame();
    }

    private void OnApplicationQuit()
    {

        Debugger.Log("Saving To File", Debugger.PriorityLevel.MustShown);
        SaveToFile();
    }


    public void ResetGameState()
    {
        gameData = new PersistentGameData();
        SaveToFile();
    }

    public bool isNewGame()
    {
        return gameData.NewGame;
    }

    public string GetLastScene()
    {
        if (string.IsNullOrEmpty(gameData.lastSceneOpen) == true)
        {
            return SceneManager.GetSceneByBuildIndex(1).name;
        }
        return gameData.lastSceneOpen;
    }

    internal List<int> GetItemCounts()
    {
        return gameData.collectedInstruments;
    }
}
