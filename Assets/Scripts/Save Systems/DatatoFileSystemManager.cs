using System;
using System.IO;


public class DatatoFileSystemManager2
{
    private string dataDirPath = "";
    private string fileName = "";

    private string fullPath = "";

    public DatatoFileSystemManager2(string path, string fileName)
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

                    writer.Write(data);

                }

            }


        }
        catch (Exception ex)
        {

        }

    }

    public PersistentGameData Load(PersistentGameData gamedata)
    {
        PersistentGameData data1 = new PersistentGameData();
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string data = "Test";

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamReader reader = new StreamReader(stream))


                    data = reader.ReadToEnd();

            }

        }
        catch (Exception ex)
        {

        }
        return data1;
    }
}
