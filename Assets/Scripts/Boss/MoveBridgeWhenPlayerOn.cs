using DG.Tweening;
using UnityEngine;

public class MoveBridgeWhenPlayerOn : MoveBridge
{
    [SerializeField] public Vector3 checkPositionOffset;
    [SerializeField] public LayerMask playerLayer;

    [SerializeField] public Vector2 groundCheckDimensions = new Vector2(1, 1);


    private bool activated = false;
    public void MoveWhenPlayerIsOn()
    {
        return;

        Collider2D collider = Physics2D.OverlapBox(transform.position + checkPositionOffset, groundCheckDimensions, 0, playerLayer);

        if (collider == null) return;

        if (activated == true) return;

        activated = true;
        DOVirtual.DelayedCall(10f, () => activated = false);
        MoveTransform();


    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + checkPositionOffset, groundCheckDimensions);
    }
}
