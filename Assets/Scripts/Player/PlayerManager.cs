using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D playerRB { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerJump playerJump { get; private set; }
    public PlayerGlide playerGlide { get; private set; }
    public PlayerGrounded playerGrounded { get; private set; }
    public PlayerAnimation playerAnimation { get; private set; }

    /// <summary>
    /// playerMovement depends on speed
    /// </summary>
    [Tooltip("Determines movement speed")]
    [SerializeField] public float speed = 50f;
    /// <summary>
    /// playerJump depends on baseJumpForce and holdJumpHeight
    /// </summary>
    [Tooltip("Determines jump height without hold")]
    [SerializeField] public float baseJumpForce = 5f;
    [Tooltip("Determines jump height from holding")]
    [SerializeField] public float holdJumpForce = 15f;
    [Tooltip("Causes player to fall earlier when jump is released")]
    [SerializeField] public bool jumpCancel = true;
    [Tooltip("Determines time elapsed before jump actually cancels")]
    [SerializeField] public float delayJumpCancelTime = 0.5f;

    /// <summary>
    /// playerGlide depends on birdBasePower, birdDecreasePowerRate, glideTime
    /// </summary>
    [Tooltip("Determines initial glide strength")]
    [SerializeField] public float birdBasePower = 5f;
    [Tooltip("Determines rate at which glide strength decreases")]
    [SerializeField] public float birdDecreasePowerRate = 1f;
    [Tooltip("Determines duration of gliding before player begins falling normally")]
    [SerializeField] public float glideTime = 2f;


    [Tooltip("Determines how fast player should fall down to trigger a falling down event")]
    [SerializeField] public float fallDownDedection = -10f;
    [SerializeField] private GameEvent fallingDownEvent;


    public bool canGlide = false;
    private bool pauseActivity = false;
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerGlide = GetComponent<PlayerGlide>();
        playerGrounded = GetComponent<PlayerGrounded>();
        playerAnimation = GetComponent<PlayerAnimation>();

    }

    private void OnEnable()
    {
        //LevelEventsManager.Instance.onPauseActivity += HandlePauseActivity;
    }
    private void OnDisable()
    {

    }

    void Start()
    {
        playerMovement.enabled = true;
        playerJump.enabled = true;
        playerGlide.enabled = true;
        playerAnimation.enabled = true;

    }

    void FixedUpdate()
    {
        if (!enabled) { return; }
        playerMovement.Move(speed);
        playerJump.Jump(baseJumpForce, holdJumpForce);
        if (canGlide) { playerGlide.Glide(birdBasePower, birdDecreasePowerRate, glideTime); }
        playerAnimation.HandlePlayerMoveAnimation(playerRB);

        Debugger.Log("playerRB.velocity.y is " + playerRB.velocity.y, Debugger.PriorityLevel.LeastImportant);
        if (playerRB.velocity.y < fallDownDedection)
        {
            //TODO fall down camera still needs more refining.
            //fallingDownEvent.TriggerEvent();
        }
    }

    public void PausePlayerActivity() { HandlePauseActivity(); enabled = false; }
    public void UnPausePlayerActivity() { enabled = true; }

    public void HandlePauseActivity()
    {
        Debug.Log("pause to idle");
        playerAnimation.SetAnimation("PlayerIdle");
        playerAnimation.currentAnimation = "PlayerIdle";
        playerAnimation.SetDirection(playerAnimation.currentDirection);
        playerRB.velocity = Physics2D.gravity;
        //pauseActivity = true;
    }

}
