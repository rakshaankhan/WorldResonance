using UnityEngine;

public class Test1 : MonoBehaviour
{

    private Rigidbody2D rigidbody2;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.sharedMaterial.friction = -10f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
