using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField]
    private float noteExpireTimer = 1f;
    private float timeElapsed = 0f;

    [SerializeField]
    public Queue<PlayerInstrument.Note> notes = new(NOTE_SIZE);
    private const int NOTE_SIZE = 4;

    [SerializeField]
    private PlayerInstrument playerInstrument;

    [SerializeField]
    private GameEvent noteListChanged;
    [SerializeField]
    private GameEvent noteListFilled;

    public void AddNote()
    {
        SpecialEnque(playerInstrument.selectedNote);

    }



    private void SpecialEnque(PlayerInstrument.Note note)
    {
        if (notes.Count >= NOTE_SIZE)
        {
            notes.Dequeue();
        }
        notes.Enqueue(note);
        NoteListChanged();
    }

    private void Update()
    {
        if (notes.Count == 0) return;

        timeElapsed += Time.deltaTime;

        if (timeElapsed > noteExpireTimer)
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
        }

    }

    public PlayerInstrument.Note GetINoteAtIndex(int index)
    {
        if (index < 0 || index >= notes.Count)
        {
            Debug.LogWarning("Index out of range");
            return default;
        }
        List<PlayerInstrument.Note> list = new(notes);
        return list[index];
    }



}
