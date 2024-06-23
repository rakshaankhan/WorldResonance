using DG.Tweening;
using UnityEngine;


public class FlyAway : MonoBehaviour
{
    [SerializeField]
    private GameEvent successGameEvent;

    [SerializeField]
    private float timer;


    [SerializeField]
    private Ease falldownEase;

    private bool succeeded;
    private bool filled;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOBlendableMoveBy(Vector3.up, timer);//.OnComplete(() => Invoke(nameof(CheckSuccess), 0.3f));
        transform.DOPunchRotation(Vector3.right, 2f);
    }

    public void OnFilledEvent()
    {
        filled = true;
        succeeded = false;
        Invoke(nameof(CheckSuccess), 0.3f);
    }

    public void OnSuccessEvent()
    {
        succeeded = true;
    }


    private void CheckSuccess()
    {
        if (succeeded == true)
        {
            transform.DOBlendableMoveBy(Vector3.up * 3, 0.5f);
            transform.DOScale(Vector3.zero, 1f);
        }
        else
        {
            transform.DOShakePosition(duration: 0.5f, strength: new Vector3(0.5f, 0f, 0f), vibrato: 30, randomness: 90, fadeOut: true);
            transform.DOMoveY(transform.position.y - 2f, 1f).SetEase(falldownEase).SetDelay(Random.Range(0, 0.3f));
            transform.DOScale(Vector3.zero, 2f);
        }


        Invoke(nameof(RemoveObject), 5f);
    }

    public void OnCancelledEvent()
    {
        if (succeeded == true) return;
        if (filled == true) return;

        transform.DOScale(Vector3.zero, 0.5f);
        Invoke(nameof(RemoveObject), 5f);
    }


    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}
