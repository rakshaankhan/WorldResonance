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

    public Note selectedNote = Note.A;

    [SerializeField]
    private List<InstrumentInformation> instrumentList;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip itemSwapSound;

    [SerializeField]
    private bool disablePlayerInstrument;

    //private void OnEnable()
    //{
    //    LevelEventsManager.Instance.onInteract += PlayCurrentInstrument;
    //}

    //private void OnDisable()
    //{
    //    LevelEventsManager.Instance.onInteract -= PlayCurrentInstrument;
    //}
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

    //Did not write actual notes to make it easier to read
    public enum Note
    {
        _,
        A,
        B,
        C,
        D

    }

    public void PlayCurrentInstrument()
    {
        instrumentPlayEvent.TriggerEvent();
    }

    public void ChangeInstrument(InstrumentType type)
    {
        if (disablePlayerInstrument == true) return;

        //TODO Maybe just use an id if there will me more insturuments instead of Type.
        audioSource.PlayOneShot(itemSwapSound);
        selectedInstrument = instrumentList[(int) type];
        instrumentChangeEvent.TriggerEvent();
    }

    public void ChooseNoteAndPlay(int id)
    {
        if (disablePlayerInstrument == true) return;

        selectedNote = (Note) id;
        PlayCurrentInstrument();
    }
}
