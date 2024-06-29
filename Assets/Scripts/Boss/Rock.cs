using UnityEngine;

public class Rock : MonoBehaviour
{

    public float torqueForce = 10f;
    public float gravityScale = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        int sign = Random.Range(0, 2) * 2 - 1; // Generates either 1 or -1
        rb.AddTorque(torqueForce * sign, ForceMode2D.Impulse);
    }
}
