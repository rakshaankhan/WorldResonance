using System.Collections.Generic;

[System.Serializable]
public class PersistentGameData
{
    // public Dictionary<string, bool> collectedItems = new Dictionary<string, bool>();

    public List<string> collectedItemGuids = new List<string>();

    // public Vector3 playerPosition;
    ///public Vector3 playerRotation;

    public bool NewGame;

    public PersistentGameData()
    {
        NewGame = true;
    }



}
