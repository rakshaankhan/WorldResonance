using DG.Tweening;
using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    [SerializeField]
    Vector3 direction;

    [SerializeField]
    Vector3 scale = Vector3.one;

    [SerializeField]
    float timer;

    [SerializeField]
    float scaletimer = 1f;

    public void MoveTransform()
    {
        transform.DOBlendableMoveBy(direction, timer);
        transform.DOScale(scale, scaletimer);
    }
}
