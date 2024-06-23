using System.Collections;
using UnityEngine;
using static Gamekit3D.RandomAudioPlayer;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private GameEvent playerWantsToFall;

    [SerializeField]
    private SoundBank jumpSounds;
    [SerializeField]
    private AudioSource audioSource;

    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    Rigidbody2D rb;

    /// <summary>
    /// Variables that determine jump behavior
    /// </summary>
    bool jumping = false;
    float elapseTime = 0;
    float maxHoldTime = 0.1f;

    bool falling;
    bool hasJumped;



    void Awake()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();

        if (playerManager.jumpCancel)
        {
            LevelEventsManager.Instance.onJumpCancel += JumpCancel;
        }
    }

    /// <summary>
    /// Called every frame to handle Jump behavior
    /// </summary>
    public void Jump(float baseJumpForce, float holdJumpForce)
    {
        if (rb.velocity.y < 0)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }

        if (playerActionManager.jumpValue && playerManager.playerGrounded.IsGrounded() && !jumping)
        {
            if (playerActionManager.moveValue.y < 0)
            {
                Debugger.Log("Player wants to fall down", Debugger.PriorityLevel.High);
                playerWantsToFall.TriggerEvent();
                return;
            }
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * baseJumpForce, ForceMode2D.Impulse);
            jumping = true;
            elapseTime = 0;
            audioSource.PlayOneShot(jumpSounds.ReturnRandom());
        }
        else if (!playerActionManager.jumpValue)
        {
            jumping = false;
        }
        else if (jumping && elapseTime < maxHoldTime && !playerManager.playerGrounded.IsGrounded())
        {
            rb.AddForce(Vector2.up * holdJumpForce * Time.deltaTime);
            elapseTime += Time.deltaTime;
        }


    }

    private void JumpCancel()
    {
        if (!playerManager.jumpCancel) { return; }

        if (!falling)
        {
            StartCoroutine(DelayJumpCancel());
        }
    }

    public IEnumerator DelayJumpCancel()
    {
        yield return new WaitForSeconds(playerManager.delayJumpCancelTime);

        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    /// <summary>
    /// Called to jump programmatically
    /// </summary>
    /// <param name="baseJumpForce"></param>
    /// <param name="holdJumpForce"></param>
    /// <param name="jumpValue">determines if jump is "held"</param>
    public void Jump(float baseJumpForce, float holdJumpForce, bool jumpValue)
    {
        if (rb.velocity.y < 0)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }

        if (jumpValue && playerManager.playerGrounded.IsGrounded() && !jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * baseJumpForce, ForceMode2D.Impulse);
            jumping = true;
            elapseTime = 0;
        }
        else if (!jumpValue)
        {
            jumping = false;
        }
        else if (jumping && elapseTime < maxHoldTime && !playerManager.playerGrounded.IsGrounded())
        {
            rb.AddForce(Vector2.up * holdJumpForce * Time.deltaTime);
            elapseTime += Time.deltaTime;
        }
    }
}
