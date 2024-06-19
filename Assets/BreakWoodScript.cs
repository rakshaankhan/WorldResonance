using System.Collections.Generic;
using UnityEngine;

public class BreakWoodScript : MonoBehaviour
{

    [SerializeField]
    private List<Rigidbody2D> woods;

    [SerializeField]
    private AudioSource audioSource;

    private bool isTriggered;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered == true) return;

        if (collision.CompareTag("Player"))
        {
            foreach (var wood in woods)
            {
                wood.constraints = RigidbodyConstraints2D.None;
            }
            audioSource.Play();
        }
    }

}
