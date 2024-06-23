using UnityEngine;

public class DisableLadder : MonoBehaviour
{
    [SerializeField] private Ladder ladder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ladder.active = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ladder.active = true;
    }
}
