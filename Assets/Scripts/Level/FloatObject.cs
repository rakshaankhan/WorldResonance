using DG.Tweening;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 floatVector;

    [SerializeField]
    private float floatFrequence;

    [SerializeField]
    private Ease curve;

    private Tweener tweener;

    private void Start()
    {
        tweener = transform.DOBlendableLocalMoveBy(floatVector, floatFrequence).SetLoops(-1, LoopType.Yoyo).SetEase(curve);
    }

    public void StopFloat()
    {
        tweener.TogglePause();

    }

    public void StartFloat()
    {
        tweener.TogglePause();
    }
}
