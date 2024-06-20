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

    void Awake()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        ApplyVerticalVelocity(speed);
        rb.velocity = moveVelocity = new Vector2(speed * playerActionManager.moveValue.x, rb.velocity.y + downVelocity);
    }
    /// <summary>
    /// Called to Move programatically
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public void Move(float speed, Vector2 direction)
    {
        ApplyVerticalVelocity(speed);
        rb.velocity = speed * direction + (rb.velocity.y + downVelocity) * Vector2.up;
    }



    private void ApplyVerticalVelocity(float speed)
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
            downVelocity = speed * baseUpVelocity * rb.gravityScale;
            Debugger.Log("Player wants to climb Ladder", Debugger.PriorityLevel.Low);
        }
    }

}
