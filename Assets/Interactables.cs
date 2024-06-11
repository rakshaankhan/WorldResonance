using UnityEngine;

public class Interactables : MonoBehaviour
{

    [SerializeField]
    private float interractionDistance = 3f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > interractionDistance) return;

        Debug.Log("Player Interracted With Me");
        transform.localScale *= 2;
    }
}
