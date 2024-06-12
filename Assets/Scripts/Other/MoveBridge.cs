using DG.Tweening;
using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float timer;


    public void MoveTransform()
    {
        transform.DOBlendableMoveBy(direction, timer);
    }
}
