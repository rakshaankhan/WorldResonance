using UnityEngine;

public class DisableParentCollider : MonoBehaviour
{
    [SerializeField]
    private Collider2D colliderToDisable;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderToDisable.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderToDisable.enabled = true;
    }
}
