using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEffects : MonoBehaviour
{

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<ContactPoint2D> contactPoints = new();
        collision.GetContacts(contactPoints);
        foreach (var point in contactPoints)
        {
            // Check if the contact normal is pointing upwards so we are hitting it from down.
            if (point.normal.y > 0)
            {
                audioSource.Play();
                break;
            }
        }
    }
}
