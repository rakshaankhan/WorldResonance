using UnityEngine;

public class CreateId : MonoBehaviour
{
    [SerializeField]
    private string id;
    private void Awake()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
