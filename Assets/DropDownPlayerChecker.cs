using UnityEngine;

public class DropDownPlayerChecker : MonoBehaviour
{

    [SerializeField]
    private Collider2D colliderToDisable;
    private bool playerIsOnMe = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIsOnMe = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsOnMe = false;
    }

    public void LetPlayerDropIfOnMe()
    {
        if (playerIsOnMe == true)
        {
            colliderToDisable.enabled = false;
        }

    }
}
