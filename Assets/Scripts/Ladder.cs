using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool active = true;

    [SerializeField]
    private SpriteRenderer visual;

    [SerializeField]
    private Material selectedMat;

    [SerializeField]
    private Material deSelectedMaterial;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active == false) return;

        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.canClimb = true;
            if (visual != null) visual.material = selectedMat;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.canClimb = false;
            if (visual != null) visual.material = deSelectedMaterial;
        }
    }
}
