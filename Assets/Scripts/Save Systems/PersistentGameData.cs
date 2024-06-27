using System.Collections.Generic;

[System.Serializable]
public class PersistentGameData
{
    // public Dictionary<string, bool> collectedItems = new Dictionary<string, bool>();

    public List<string> collectedItemGuids = new List<string>();

    public string lastSceneOpen;

    public List<int> collectedInstruments = new List<int> { 0, 0, 0 };

    // public Vector3 playerPosition;
    ///public Vector3 playerRotation;

    public bool NewGame;

    public PersistentGameData()
    {
        NewGame = true;
    }



}
