using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Gamekit3D.RandomAudioPlayer;

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
    private Light2D motorLight;


    [Header("Sound Effects")]
    [SerializeField]
    private AudioSource walkSound;
    [SerializeField]
    private AudioSource climbSound;
    [SerializeField]
    private AudioSource motorSound;
    [SerializeField]
    private AudioSource moveStopSound;
    [SerializeField]
    private AudioSource generalSounds;

    [SerializeField]
    private SoundBank walkSounds;


    [SerializeField]
    private SoundBank motorSounds;

    private float walkingTimer = 0;

    private PlayerGrounded playerGrounded;
    void Awake()
    {
        playerGrounded = GetComponent<PlayerGrounded>();
        playerActionManager = GetComponent<PlayerActionManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        walkingTimer -= motorSounds.clips[0].length;
        motorSound.Pause();
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
            //   walkSound.UnPause();

            if (walkSound.isPlaying == false && playerGrounded.IsGrounded())
            {
                var clip = walkSounds.ReturnRandom();
                walkSound.clip = clip;
                walkSound.Play();
            }
            if (walkingTimer < 0)
            {

                moveStopSound.Stop();
                generalSounds.PlayOneShot(motorSounds.clips[0]);
                walkingTimer = 0;
            }
            else if (walkingTimer > motorSounds.clips[0].length)
            {
                if (motorSound.isPlaying == false)
                {
                    Debugger.Log("MotorSound.UnPause called", Debugger.PriorityLevel.Medium);
                    motorSound.UnPause();
                }

            }
            motorLight.shapeLightFalloffSize = Mathf.Min(2, motorLight.shapeLightFalloffSize + Time.deltaTime / 3f);
            walkingTimer += Time.fixedDeltaTime;

        }
        else
        {
            walkSound.Stop();
            //motorSound.loop = false;

            if (walkingTimer > motorSounds.clips[0].length)
            {
                moveStopSound.Play();
            }
            motorSound.Pause();
            motorLight.shapeLightFalloffSize = Mathf.Max(0, motorLight.shapeLightFalloffSize - Time.deltaTime);
            walkingTimer -= motorSounds.clips[0].length;
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

            Debugger.Log("Player wants to climb Ladder", Debugger.PriorityLevel.Low);
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
