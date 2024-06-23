using Unity.Cinemachine;
using UnityEngine;

public class ParalaxMethod1 : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField]
    private float paralaxEffectModifier;

    [SerializeField]
    private bool IsInfinite = true;

    private float lenght, start;

    private Camera cam;
    void Start()
    {
        start = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main;
        CinemachineCore.CameraUpdatedEvent.AddListener(UpdateParallax);
    }


    void UpdateParallax(CinemachineBrain brainssssss)
    {
        float temp = cam.transform.position.x * (1 - paralaxEffectModifier);
        float dist = cam.transform.position.x * paralaxEffectModifier;

        transform.position = new Vector3(start + dist, transform.position.y, transform.position.z);


        if (IsInfinite == false) return;

        if (temp > start + lenght)
        {
            start += lenght;
        }
        else if (temp < start - lenght)
        {
            start -= lenght;
        }

    }
}
