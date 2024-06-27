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

    private PlayerAnimation playerAnimation;

    //private void OnEnable()
    //{
    //    LevelEventsManager.Instance.onInteract += PlayCurrentInstrument;
    //}

    //private void OnDisable()
    //{
    //    LevelEventsManager.Instance.onInteract -= PlayCurrentInstrument;
    //}

    private Inventory inventory;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        inventory = GetComponent<Inventory>();
    }
    private void Start()
    {

        CheckInventoryForInstruments();
    }

    public enum InstrumentType
    {
        /// <summary>
        /// Creates Stuff
        /// </summary>
        String,
        /// <summary>
        /// Removes Stuff
        /// </summary>
        Percussion,
        /// <summary>
        /// Moves Stuff
        /// </summary>
        Wind
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
        playerAnimation.PlayInstrument(selectedInstrument.instrumentType);
    }

    public void ChangeInstrument(InstrumentType type)
    {
        if (disablePlayerInstrument == true) return;

        if (inventory.GetItemCount((int) type) == 0) return;

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

    public void CheckInventoryForInstruments()
    {
        if (inventory.GetSum() <= 0)
        {
            disablePlayerInstrument = true;
        }
        else
        {
            disablePlayerInstrument = false;
        }
    }
}
