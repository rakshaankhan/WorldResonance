using UnityEngine;

public class MusicSheet : MonoBehaviour
{
    [SerializeField]
    private float shakeDuration = 0.01f;

    public void FailEvent()
    {

        //transform.DOBlendableLocalMoveBy(Vector3.right, shakeDuration).SetLoops(20, LoopType.Yoyo);
    }


    public void OnSuccessEvent()
    {

        //transform.DOBlendableLocalMoveBy(Vector3.right, shakeDuration).SetLoops(20, LoopType.Yoyo);
    }

}
