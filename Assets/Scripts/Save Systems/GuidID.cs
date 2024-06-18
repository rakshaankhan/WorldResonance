using UnityEngine;



public class GuidID : MonoBehaviour
{
    [Tooltip("Do Not Use Context Menu")]
    [SerializeField]

    public string id;



    [ContextMenu("Create New Guid")]
    public void CreateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
