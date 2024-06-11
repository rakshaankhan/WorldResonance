using System.Collections.Generic;
using UnityEngine;

public class PlayerInstrument : MonoBehaviour
{

    [SerializeField]
    private GameEvent instrumentPlayEvent;

    [SerializeField]
    private GameEvent instrumentChangeEvent;

    [SerializeField]
    public InstrumentInformation selectedInstrument;

    [SerializeField]
    private List<InstrumentInformation> instrumentList;

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
        instrumentPlayEvent.TriggerEvent();
    }

    public void ChangeInstrument(InstrumentType type)
    {
        //TODO Maybe just use an id if there will me more insturuments instead of Type.
        selectedInstrument = instrumentList[(int) type];
        instrumentChangeEvent.TriggerEvent();
    }
}
