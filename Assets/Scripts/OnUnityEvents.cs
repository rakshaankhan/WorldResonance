using UnityEngine;
using UnityEngine.Events;

public class OnUnityEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onDisableEvents;

    [SerializeField]
    private UnityEvent onEnableEvents;

    [SerializeField]
    private UnityEvent onTriggerEnterEvents;




    public void OnDisable()
    {
        onDisableEvents.Invoke();
    }

    public void OnEnable()
    {
        onEnableEvents.Invoke();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnterEvents.Invoke();
    }

}
