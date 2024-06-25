using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    /// <summary>
    /// Transform under player
    /// </summary>
    [SerializeField] public Transform groundCheck;
    /// <summary>
    /// radius of ground check
    /// </summary>
    [SerializeField] public Vector2 groundCheckDimensions = new Vector2(1, 1);
    /// <summary>
    /// layers that are considered "ground"
    /// </summary>
    [SerializeField] public LayerMask groundCheckLayerMask;

    /// <summary>
    /// returns true if player is touching ground, else returns false
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapBox(groundCheck.position, groundCheckDimensions, 0, groundCheckLayerMask);
        if (collider == null)
        {
            return false;
        }

        //TODO Do we need normals?
        //else
        //{
        //    Vector3 closestPoint = collider.ClosestPoint(transform.position);
        //    Vector3 positionDifference = (closestPoint - transform.position);
        //    Vector3 overlapDirection = positionDifference.normalized;
        //    Debugger.Log("overlapDirection " + overlapDirection, Debugger.PriorityLevel.High);
        //    if (overlapDirection.y < 0)
        //    {
        //        return true;
        //    }

        //}
        //return false;
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckDimensions);
    }
}
