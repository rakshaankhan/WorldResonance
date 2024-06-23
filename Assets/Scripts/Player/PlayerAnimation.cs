using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerActionManager playerActionManager;
    PlayerManager playerManager;
    SpriteRenderer sprite;
    public Animator playerAnimator;
    public string currentAnimation = "PlayerIdle";
    string[] directions = { "Left", "Right" };
    public string currentDirection = "Right";
    void Awake()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        playerManager = GetComponent<PlayerManager>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void PlayAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) { return; }
        currentAnimation = newAnimation;
        playerAnimator?.Play(currentAnimation);
    }

    public void SetDirection(string direction)
    {
        currentDirection = direction;
        ChangeDirection(direction);
    }

    private void ChangeDirection(string direction)
    {
        if (direction == "Right")
        {
            FlipSprite(false);

        }
        else
        {
            FlipSprite(true);

        }
    }

    private void FlipSprite(bool flag)
    {
        //sprite.flipX = flag;
        if (flag) { transform.localScale = new Vector3(-1, 1, 1); } else { transform.localScale = Vector3.one; }
    }

    public void SetAnimation(string animation) { playerAnimator.Play(animation); }

    public void HandlePlayerMoveAnimation()
    {
        //Debug.Log("Handle player animation");
        if (playerManager.playerGlide.gliding) { SetAnimationGlide(); }
        else if (Mathf.Abs(playerActionManager.moveValue.x) > 0) { SetAnimationMove(); }
        else { SetAnimationIdle(); }
    }

    public void SetAnimationMove()
    {
        if (playerActionManager.moveValue.x > 0) { SetAnimationMoveRight(); }
        else { SetAnimationMoveLeft(); }
    }
    public void SetAnimationIdle()
    {
        if (currentDirection == "Left") { SetAnimationIdleLeft(); }
        else { SetAnimationIdleRight(); }
        //Debug.Log("idling");
    }
    public void SetAnimationGlide()
    {
        if (playerActionManager.moveValue.x < 0) { SetAnimationGlideLeft(); }
        else if (playerActionManager.moveValue.x > 0) { SetAnimationGlideRight(); }
        else if (playerActionManager.moveValue.x == 0)
        {
            if (currentDirection == "Left") { SetAnimationGlideLeft(); }
            else { SetAnimationGlideRight(); }
        }
    }

    public void SetAnimationMoveRight()
    {
        currentDirection = directions[1];
        FlipSprite(false);
        PlayAnimation("PlayerMove");
    }

    public void SetAnimationMoveLeft()
    {
        currentDirection = directions[0];
        FlipSprite(true);
        PlayAnimation("PlayerMove");
    }

    public void SetAnimationIdleRight()
    {
        FlipSprite(false);
        currentDirection = directions[1];
        PlayAnimation("PlayerIdle");
        //Debug.Log("idle right");
    }
    public void SetAnimationIdleLeft()
    {
        FlipSprite(true);
        currentDirection = directions[0];
        PlayAnimation("PlayerIdle");
        //Debug.Log("idle left");
    }

    public void SetAnimationGlideRight()
    {
        FlipSprite(false);
        currentDirection = directions[1];
        PlayAnimation("PlayerGlide");
    }
    public void SetAnimationGlideLeft()
    {
        FlipSprite(true);
        currentDirection = directions[0];
        PlayAnimation("PlayerGlide");
    }



}
