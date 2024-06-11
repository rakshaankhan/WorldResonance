using UnityEngine;

public class PlayerInstrument : MonoBehaviour
{

    [SerializeField]
    private GameEvent instrumentEvent;

    public enum InstrumentType
    {
        /// <summary>
        /// Moves Stuff
        /// </summary>
        Wind,
        /// <summary>
        /// Removes Stuff
        /// </summary>
        Percussion,
        /// <summary>
        /// Creates Stuff
        /// </summary>
        String
    }

    public void PlayCurrentInstrument()
    {
        instrumentEvent.TriggerEvent();
    }
}
