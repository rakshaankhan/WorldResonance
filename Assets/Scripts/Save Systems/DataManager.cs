using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    private PersistentGameData data;

    private List<IDataSaveLoad> objectsWithSaveLoad;

    private DatatoFileSystemManager2 fileSystem;


    public void LoadGame()
    {
        foreach (var persistenObject in objectsWithSaveLoad)
        {
            persistenObject.Load(data);
        }

    }

    public void SaveGame()
    {
        foreach (var persistenObject in objectsWithSaveLoad)
        {
            persistenObject.Save(data);
        }
    }

    public void FindAllPersistentObjectsInScene()
    {
        IEnumerable<IDataSaveLoad> objectsWithSaveLoadTemp = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaveLoad>();
        objectsWithSaveLoad = new List<IDataSaveLoad>(objectsWithSaveLoadTemp);

    }

    public void SaveToFile()
    {
        fileSystem.Save(data);
    }

    public void LoadFromFile()
    {
        data = fileSystem.Load(data);
    }

}
