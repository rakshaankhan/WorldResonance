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
    List<PlayerInstrument.Note> acceptedNoteOrder;

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
        bool flag = true;

        for (int i = 0; i < acceptedNoteOrder.Count; i++)
        {
            flag = acceptedNoteOrder[i] == noteManager.GetINoteAtIndex(i);
            if (flag == false) break;
        }

        if (flag)
        {
            //transform.DOBlendableScaleBy(Vector3.one * 3f, 3f);
            SpecialAction();
        }



    }

    private void SpecialAction()
    {
        specialInteraction.Invoke();
    }

}
