using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool active = true;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active == false) return;

        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.canClimb = false;
        }
    }
}
