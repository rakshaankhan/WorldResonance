using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactables : MonoBehaviour
{

    [SerializeField]
    private float interractionDistance = 3f;

    [SerializeField]
    private NoteManager noteManager;

    [SerializeField]
    private PlayerInstrument.InstrumentType insturumentType = default;

    [SerializeField]
    List<PlayerInstrument.Note> acceptedNoteOrder;

    [SerializeField]
    private GameEvent songFailEvent;
    [SerializeField]
    private GameEvent songSuccessEvent;

    [SerializeField]
    private UnityEvent specialInteraction;

    private GameObject player;
    private PlayerInstrument playerInstrument;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInstrument = player.GetComponent<PlayerInstrument>();
    }



    public void Interact()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > interractionDistance) return;

        if (acceptedNoteOrder.Count < 6) return;
        if (noteManager.notes.Count < 6) return;
        byte mistakeCount = 0;

        //TODO right now this assumes player did not change instrument in mid song play. I am not sure what will be the final intend. 
        if (playerInstrument.selectedInstrument.instrumentType != insturumentType) mistakeCount++;

        for (int i = 0; i < acceptedNoteOrder.Count; i++)
        {
            if (acceptedNoteOrder[i] != noteManager.GetINoteAtIndex(i)) mistakeCount++;
        }

        if (mistakeCount == 0)
        {
            songSuccessEvent.TriggerEvent();
            SpecialAction();
        }
        else
        {
            songFailEvent.TriggerEvent();
        }
    }

    private void SpecialAction()
    {
        specialInteraction.Invoke();
    }

}
