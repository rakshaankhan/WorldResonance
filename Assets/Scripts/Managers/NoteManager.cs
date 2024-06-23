using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.RandomAudioPlayer;

public class NoteManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform musicSheetUI;

    //[SerializeField]
    //private List<AudioClip> noteAudioClips;

    [SerializeField]
    private List<SoundBank> soundBanks;
    [SerializeField]
    private List<SoundBank> textureSoundBanks;

    [SerializeField]
    private float noteExpireTimer = 1f;
    [SerializeField]
    private float noteUnfilledDismisTimer = 0.2f;
    [SerializeField]
    private float noteFilledExpireTimer = 1f;
    private float timeElapsed = 0f;

    [SerializeField]
    public Queue<PlayerInstrument.Note> notes = new(NOTE_SIZE);
    private const int NOTE_SIZE = 6;

    [SerializeField]
    private PlayerInstrument playerInstrument;

    [SerializeField]
    private GameEvent noteListChanged;
    [SerializeField]
    private GameEvent noteListFilled;
    [SerializeField]
    private GameEvent noteListCleared;

    private bool listFilled = false;

    private bool addingNewNote;

    [SerializeField]
    private AudioSource audioSourceNotes;

    [Range(0f, 0.2f)]
    [SerializeField]
    private float pitchRandomRangeForTextures;
    [SerializeField]
    private AudioSource audioSourceTextures;

    private void Start()
    {
        if (playerInstrument == null)
        {
            playerInstrument = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInstrument>();
        }

        if (musicSheetUI == null)
        {

            musicSheetUI = GameObject.FindGameObjectWithTag("MusicSheet").GetComponent<RectTransform>();
        }
    }

    public void AddNote()
    {
        SpecialEnque(playerInstrument.selectedNote);

    }



    public void SpecialEnque(PlayerInstrument.Note note)
    {
        if (note == PlayerInstrument.Note._) return;
        if (listFilled == true) return;

        musicSheetUI.DOScaleX(1f, 0.2f);
        if (notes.Count >= NOTE_SIZE)
        {
            notes.Dequeue();
        }
        int instrumentIndex = (int) playerInstrument.selectedInstrument.instrumentType;

        SoundBank instrumentNotes = soundBanks[instrumentIndex];
        var ClipToPlay = instrumentNotes.ReturnRandomFromVariations((int) (note) - 1);
        audioSourceNotes.PlayOneShot(ClipToPlay);

        SoundBank textureSounds = textureSoundBanks[instrumentIndex];
        var textureToPLay = textureSounds.ReturnRandomFromVariations((int) (note) - 1);
        audioSourceTextures.pitch = Random.Range(1.0f - pitchRandomRangeForTextures, 1.0f + pitchRandomRangeForTextures);
        audioSourceTextures.PlayOneShot(textureToPLay);

        Debug.Log(ClipToPlay.name);
        notes.Enqueue(note);
        addingNewNote = true;
        NoteListChanged();
    }

    private void Update()
    {
        if (notes.Count == 0) return;

        timeElapsed += Time.deltaTime;

        if (listFilled == true && timeElapsed > noteFilledExpireTimer)
        {
            notes.Clear();
            listFilled = false;
            musicSheetUI.DOScaleX(0f, 0.2f);
            NoteListChanged();
        }

        if (timeElapsed > noteExpireTimer)
        {
            addingNewNote = false;

            if (listFilled == false)
            {
                notes.Dequeue();
                NoteListChanged();
            }
        }

        if (addingNewNote == false && timeElapsed > noteUnfilledDismisTimer)
        {
            notes.Dequeue();
            NoteListChanged();
        }
    }

    private void NoteListChanged()
    {
        timeElapsed = 0f;
        noteListChanged.TriggerEvent();
        if (notes.Count == NOTE_SIZE)
        {
            noteListFilled.TriggerEvent();
            listFilled = true;
        }

        if (notes.Count == 0)
        {
            noteListCleared.TriggerEvent();
            musicSheetUI.DOScaleX(0f, 0.2f);
        }

    }

    public PlayerInstrument.Note GetINoteAtIndex(int index)
    {
        if (index < 0 || index >= notes.Count)
        {
            Debug.LogWarning("Index out of range, index is " + index);
            return default;
        }
        List<PlayerInstrument.Note> list = new(notes);
        return list[index];
    }

    public bool IsLastNote(int index)
    {
        if (addingNewNote == false) return false;

        return notes.Count == index + 1;
    }


    public void OnSuccessEvent()
    {
        foreach (var note in notes)
        {
            //audioSource.PlayOneShot(noteAudioClips[((int) note - 1)]);
            // audioSource.do
        }

    }

}
