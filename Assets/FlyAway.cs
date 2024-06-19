using DG.Tweening;
using UnityEngine;


public class FlyAway : MonoBehaviour
{

    [SerializeField]
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOBlendableMoveBy(Random.insideUnitSphere, timer).OnComplete(() => Destroy(gameObject));
    }


}
