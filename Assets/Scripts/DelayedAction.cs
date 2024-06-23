using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedAction : MonoBehaviour
{
    [SerializeField]
    private UnityEvent delayedEvent;

    [SerializeField]
    private float delayTime = 1f;



    private IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);
        delayedEvent.Invoke();
    }

}
