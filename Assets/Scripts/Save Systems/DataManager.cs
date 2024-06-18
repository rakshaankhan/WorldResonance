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
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += AfterSceneLoad;

        //Application.quitting +=
    }

    private void Start()
    {
        fileSystem = new DatatoFileSystemManager(path, fileName);
        LoadFromFile();
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

        Debug.Log("Saving To File");
        SaveToFile();
    }


    public void ResetGameState()
    {
        gameData = new PersistentGameData();
        SaveToFile();
    }
}
