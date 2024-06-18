using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class PersistentGameData
{
    public Dictionary<string, bool> collectedItems = new Dictionary<string, bool>();

    public Vector3 plyerPosition;
    public Vector3 playerRotation;

    public bool NewGame;

    public PersistentGameData()
    {
        NewGame = true;
    }



}
