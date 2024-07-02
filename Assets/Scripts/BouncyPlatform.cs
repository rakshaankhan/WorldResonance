using DG.Tweening;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{


    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private GameObject visual;


    [Range(10f, 0.5f)]
    [SerializeField]
    private float punchAnimationStrenght = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debugger.Log("Player enter collision with " + gameObject.name, Debugger.PriorityLevel.MustShown);
        //collision.GetComponent<PlayerJump>().SetJump();
        jumpSound.Play();
        visual.transform.DOPunchScale(Vector3.up / punchAnimationStrenght, 0.3f, vibrato: 1, 0.1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<PlayerJump>().SetJump();
    }
}
