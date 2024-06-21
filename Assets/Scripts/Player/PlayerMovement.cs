using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    Rigidbody2D rb;
    public Vector2 moveVelocity { get; private set; }
    [SerializeField] public float baseUpVelocity = 0.02f;
    [SerializeField] public float baseDownVelocity = -.1f;
    float downVelocity = 0;

    public bool canClimb = false;

    [SerializeField]
    private AudioSource walkSound;
    [SerializeField]
    private AudioSource climbSound;
    void Awake()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        ManageVerticalMovement(speed);
        rb.velocity = moveVelocity = new Vector2(speed * playerActionManager.moveValue.x, rb.velocity.y + downVelocity);
        ApplyEffects();
    }



    /// <summary>
    /// Called to Move programatically
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public void Move(float speed, Vector2 direction)
    {
        ManageVerticalMovement(speed);
        rb.velocity = speed * direction + (rb.velocity.y + downVelocity) * Vector2.up;
        ApplyEffects();
    }

    private void ApplyEffects()
    {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            walkSound.UnPause();
        }
        else
        {
            walkSound.Pause();
        }

    }

    private void ManageVerticalMovement(float speed)
    {
        if (playerActionManager.moveValue.y < 0)
        {
            downVelocity = baseDownVelocity * speed;
        }
        else
        {
            downVelocity = 0;
        }

        if (canClimb && playerActionManager.moveValue.y > 0)
        {
            //downVelocity = speed * baseUpVelocity * rb.gravityScale;
            Debugger.Log("Player wants to climb Ladder", Debugger.PriorityLevel.Low);
            // climbSound.pitch = Mathf.Clamp(downVelocity, 1.2f, 3f);
            climbSound.UnPause();
            rb.isKinematic = true;
            rb.MovePosition(transform.position + Vector3.up * speed * baseUpVelocity + Vector3.right * speed * playerActionManager.moveValue.x * baseUpVelocity);
        }
        else
        {
            climbSound.Pause();
            rb.isKinematic = false;
        }
    }


}
