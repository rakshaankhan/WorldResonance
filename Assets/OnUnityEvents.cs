using UnityEngine;
using UnityEngine.Events;

public class OnUnityEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onDisableEvents;

    [SerializeField]
    private UnityEvent onEnableEvents;


    public void OnDisable()
    {
        onDisableEvents.Invoke();
    }

    public void OnEnable()
    {
        onEnableEvents.Invoke();
    }
}
