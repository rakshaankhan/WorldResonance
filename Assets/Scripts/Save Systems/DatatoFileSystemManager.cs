using System;
using System.IO;
using UnityEngine;


public class DatatoFileSystemManager
{
    private string dataDirPath = "";
    private string fileName = "";

    private string fullPath = "";

    public DatatoFileSystemManager(string path, string fileName)
    {
        this.dataDirPath = path;
        this.fileName = fileName;
        fullPath = Path.Combine(path, fileName);
    }
    public void Save(PersistentGameData gameData)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string data = "Test";

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // data = JsonFormatter.SerializeObject(gameData);
                    data = JsonUtility.ToJson(gameData);
                    writer.Write(data);

                }

            }


        }
        catch (Exception ex)
        {

        }

    }

    public PersistentGameData Load()
    {
        PersistentGameData GameData = new PersistentGameData();
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string data = "Test";

            using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream))


                    data = reader.ReadToEnd();
                // GameData = (PersistentGameData) (JsonFormatter.DeserializeObject(data, typeof(PersistentGameData)) ?? GameData);
                GameData = JsonUtility.FromJson<PersistentGameData>(data) ?? GameData;

            }

        }
        catch (Exception ex)
        {

        }
        return GameData;
    }
}
