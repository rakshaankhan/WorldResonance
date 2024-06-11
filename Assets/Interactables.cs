using UnityEngine;

public class Interactables : MonoBehaviour
{

    [SerializeField]
    private float interractionDistance = 3f;



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

        Debug.Log("Player Interracted With Me");
        var currentInstrumentType = playerInstrument.selectedInstrument.instrumentType;
        switch (currentInstrumentType)
        {
            case PlayerInstrument.InstrumentType.Wind:
            transform.transform.position += Vector3.up / 2;
            break;
            case PlayerInstrument.InstrumentType.Percussion:
            transform.localScale /= 2;
            break;
            case PlayerInstrument.InstrumentType.String:
            transform.localScale *= 2;
            break;
            default:
            break;
        }

    }
}
